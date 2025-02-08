using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Rewardergg.Application.Configurations;
using Rewardergg.Application.DTOs;
using Rewardergg.Application.Interfaces;
using Rewardergg.Application.Models;
using Rewardergg.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rewardergg.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly StartggSettings _startggSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private string _grantType = "authorization_code";
        private string _scope = "user.identity user.email";
        private readonly string _oauthEndpoint = "/oauth/access_token";
        private readonly JwtSettings _jwtSettings;

        public AuthService(HttpClient httpClient, IOptionsMonitor<StartggSettings> startggSettings, IOptionsMonitor<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _startggSettings = startggSettings.CurrentValue;
            _jwtSettings = jwtSettings.CurrentValue;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OauthResponseDto> AuthenticateWithOauth(string code)
        {
            var postData = new
            {
                grant_type = _grantType,
                client_secret = _startggSettings.ClientSecret,
                code,
                scope = _scope,
                client_id = _startggSettings.ClientId,
                redirect_uri = _startggSettings.RedirectUrl
            };

            var json = JsonConvert.SerializeObject(postData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_oauthEndpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to authenticate {response.StatusCode}: {error}");
            }

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<OauthResponseDto>(responseString) ?? throw new InvalidOperationException("Deserialization failed.");

        }

        public string CreateUserToken(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                    new Claim("gamerTag", user.GamerTag!),
                    new Claim("discriminator", user.Discriminator!)
                };


            foreach (var role in user.Roles!)
            {
                var claim = new Claim(ClaimTypes.Role, role);
                claims.Add(claim);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

        public string GetSessionUser()
        {
            var username = _httpContextAccessor.HttpContext!.User?.Claims?
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return username!;
        }
    }
}
