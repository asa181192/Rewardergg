using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rewardergg.Application.DTOs;
using Rewardergg.Application.Interfaces;
using Rewardergg.Application.Models;
using Rewardergg.Domain;
using Rewardergg.Domain.Enums;
using Rewardergg.Infrastructure.Persitence;

namespace Rewardergg.Application.Services
{
    public class AuthWorkflowService : IAuthWorkflowService
    {
        private AppDbContext _appDbContext;
        private IAuthService _authService;
        private IStartggService _startggService;
        private ILogger<AuthWorkflowService> _logger;

        public AuthWorkflowService(IAuthService authService, AppDbContext appDbContext, IStartggService startggService, ILogger<AuthWorkflowService> logger)
        {
            _authService = authService;
            _appDbContext = appDbContext;
            _startggService = startggService;
            _logger = logger;
        }

        public async Task<AuthResponse> LoginAsync(string code, CancellationToken cancellationToken)
        {
            // Step 1: Authenticate with OAuth
            var oauthResponse = await _authService.GetTokenWithAuthorizationCodeAsync(code);

            if (string.IsNullOrEmpty(oauthResponse?.access_token) || string.IsNullOrEmpty(oauthResponse?.refresh_token))
                throw new Exception("OAuth authentication failed. Tokens are missing.");

            // Step 2: Fetch user data from Start.gg
            var accountData = await _startggService.GetPlayerAccountData(oauthResponse.access_token, cancellationToken);

            if (accountData?.currentUser?.discriminator == null)
                throw new Exception("Start.gg user data is incomplete.");

            // Step 3: Check if user exists
            var user = await _appDbContext.Users.
                FirstOrDefaultAsync(x => x.Discriminator == accountData.currentUser.discriminator, cancellationToken);

            if (user == null)
            {
                user = new User
                {
                    Name = accountData.currentUser.name,
                    Email = accountData.currentUser.email,
                    UserId = accountData.currentUser.id,
                    GamerTag = accountData.currentUser.player?.gamerTag,
                    Discriminator = accountData.currentUser.discriminator,
                    IsActive = true,
                    Roles = new List<string> { AppRole.Participant.ToString() },
                    Points = 0
                };

                await _appDbContext.Users.AddAsync(user, cancellationToken);
            }
            else
            {
                // Step 4: Update email if it has changed in Start.gg
                if (user.Email != accountData.currentUser.email)
                {
                    user.Email = accountData.currentUser.email;
                    _appDbContext.Users.Update(user);
                }
            }

            var existingToken = await _appDbContext.UserToken
                .FirstOrDefaultAsync(t => t.UserId == user.Id, cancellationToken);

            UserToken? token = null; 

            if (existingToken == null)
            {
                token = new UserToken
                {
                    UserId = user.Id,
                    AccessToken = oauthResponse.access_token,
                    RefreshToken = oauthResponse.refresh_token,
                    ExpiresAt = DateTime.UtcNow.AddSeconds(oauthResponse.expires_in),
                    PlatformRefreshToken = Guid.NewGuid().ToString()
                };
                await _appDbContext.UserToken.AddAsync(token, cancellationToken);
            }
            else
            {
                existingToken.AccessToken = oauthResponse.access_token;
                existingToken.RefreshToken = oauthResponse.refresh_token;
                existingToken.ExpiresAt = DateTime.UtcNow.AddSeconds(oauthResponse.expires_in);
                existingToken.PlatformRefreshToken = Guid.NewGuid().ToString();
                _appDbContext.UserToken.Update(existingToken);
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);

            // Step 5: Generate JWT with claims
            var jwt = _authService.CreateUserToken(user);

            return new AuthResponse
            {
                Jwt = jwt,
                RefreshToken = existingToken == null ? token!.PlatformRefreshToken : existingToken.PlatformRefreshToken,
                ExpiresAt = _authService.GetJwtExpirationTime()
            };
        }

        public async Task<AuthResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var storedToken = await _appDbContext.UserToken
                                .Include(t => t.User)
                                .FirstOrDefaultAsync(t => t.PlatformRefreshToken == refreshToken, cancellationToken);

            if (storedToken == null)
                return null; // Invalid refresh token needs full OAuth login

            if (storedToken.User == null)
                throw new Exception("User associated with the token not found.");

            var now = DateTime.UtcNow;
            var refreshThreshold = TimeSpan.FromDays(1);

            if (storedToken.ExpiresAt < now)
            {
                // Cannot refresh: user needs full OAuth login
                return null;
            }

            if (storedToken.ExpiresAt - now < refreshThreshold)
            {
                var oauthResponse = await _authService.GetTokenWithAuthorizationCodeAsync(storedToken.RefreshToken, true);

                if (string.IsNullOrEmpty(oauthResponse?.access_token) || string.IsNullOrEmpty(oauthResponse?.refresh_token))
                    throw new Exception("OAuth authentication failed. Tokens are missing.");

                storedToken.AccessToken = oauthResponse.access_token;
                storedToken.RefreshToken = oauthResponse.refresh_token;
                storedToken.ExpiresAt = DateTime.UtcNow.AddSeconds(oauthResponse.expires_in);
                _appDbContext.UserToken.Update(storedToken);

                await _appDbContext.SaveChangesAsync(cancellationToken);
            }

            // Generate new JWT for the platform
            var newJwt = _authService.CreateUserToken(storedToken.User);

            return new AuthResponse
            {
                Jwt = newJwt,
                RefreshToken = storedToken.PlatformRefreshToken,
                ExpiresAt = _authService.GetJwtExpirationTime()
            };
        }

        public async Task SyncUserDataAsync(Guid userId, CancellationToken cancellationToken)
        {
            //var user = await _appDbContext.Users
            //    .Include(u => u.EventParticipations)
            //    .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            //if (user == null)
            //    throw new Exception("User not found.");

            //var oauthToken = await _appDbContext.UserToken
            //    .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

            //if (oauthToken == null)
            //    throw new Exception("OAuth token not found.");

            //var events = await _appDbContext.UserEnrollments
            //    .Where(ue => ue.UserId == userId)
            //    .Include(ue => ue.Event)
            //    .ThenInclude(e => e.Tournament)
            //    .ToListAsync(cancellationToken);

            //foreach (var ev in events)
            //{
            //    // Call Start.gg for entrant standing
            //    var standing = await _startggService.GetEntrantStandingAsync(
            //        oauthToken.AccessToken,
            //        ev.Event.Id.ToString(),
            //        user.UserId, // or userId if that's the correct identifier for Start.gg
            //        cancellationToken);

            //    //if (standing != null && standing.IsFinal && standing.Placement.HasValue)
            //    //{
            //    //    var eventUser = await _appDbContext.EventParticipants
            //    //        .FirstOrDefaultAsync(ep => ep.EventId == ev.Event.Id && ep.UserId == userId, cancellationToken);

            //    //    if (eventUser == null)
            //    //    {
            //    //        eventUser = new EventUser
            //    //        {
            //    //            EventId = ev.Event.Id,
            //    //            UserId = userId,
            //    //            Placement = standing.Placement.Value,
            //    //            TotalWins = 0,
            //    //            TotalLosses = 0
            //    //        };
            //    //        await _appDbContext.EventParticipants.AddAsync(eventUser, cancellationToken);
            //    //    }
            //    //    else
            //    //    {
            //    //        eventUser.Placement = standing.Placement.Value;
            //    //        _appDbContext.EventParticipants.Update(eventUser);
            //    //    }
            //    //}
            //}

            //await _appDbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
