using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rewardergg.Application.Interfaces;
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

        public async Task<string> LoginAsync(string code, CancellationToken cancellationToken)
        {
            // Step 1: Authenticate with OAuth
            var oauthResponse = await _authService.AuthenticateWithOauth(code);

            _logger.LogInformation("Oauth response {oauthResponse}", oauthResponse.ToString());

            if (string.IsNullOrEmpty(oauthResponse?.access_token) || string.IsNullOrEmpty(oauthResponse?.refresh_token))
                throw new Exception("OAuth authentication failed. Tokens are missing.");

            // Step 2: Fetch user data from Start.gg
            var accountData = await _startggService.GetPlayerAccountData(oauthResponse.access_token);

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
                    GamerTag = accountData.currentUser.player?.gamerTag,
                    Discriminator = accountData.currentUser.discriminator,
                    IsActive = true,
                    Roles = new List<string>() { AppRole.Participant.ToString() },
                    Points = 0

                };

                await _appDbContext.Users.AddAsync(user);
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

            // Step 5: Save OAuth tokens
            var token = new UserToken
            {
                UserId = user.Id,
                AccessToken = oauthResponse.access_token,
                ExpiresAt = oauthResponse.expires_in,
                RefreshToken = oauthResponse.refresh_token
            };
            await _appDbContext.UserToken.AddAsync(token, cancellationToken);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            // Step 6: Generate JWT
            var jwt = _authService.CreateUserToken(user);

            return jwt;
        }

    }
}
