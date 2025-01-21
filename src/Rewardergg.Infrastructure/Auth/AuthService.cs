using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rewardergg.Application.DTOs;
using Rewardergg.Application.Interfaces;
using System.Text;

namespace Rewardergg.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private string _grantType = "authorization_code";
        private string _scope = "user.identity user.email";
        private readonly string _oauthEndpoint = "/oauth/access_token";

        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<OauthResponseDto> AuthenticateWithOauth(string code)
        {
            var postData = new
            {
                grant_type = _grantType,
                client_secret = _configuration["Startgg:ClientSecret"],
                code,
                scope = _scope,
                client_id = _configuration["Startgg:ClientId"],
                redirect_uri = _configuration["Startgg:RedirectUrl"]
            };

            var json = JsonConvert.SerializeObject(postData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_oauthEndpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error {response.StatusCode}: {error}");
            }

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<OauthResponseDto>(responseString) ?? throw new InvalidOperationException("Deserialization failed.");

        }

        public Task CreateUserToken()
        {
            throw new NotImplementedException();
        }
    }
}
