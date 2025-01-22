using Microsoft.AspNetCore.Mvc;
using Rewardergg.Application.Interfaces;

namespace Rewardergg.Api.Controllers
{
    [ApiController]
    [Route("~/api/v1/login")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("oauth")]
        public async Task<IActionResult> OauthLogin(string code)
        {
            var token = await _authService.AuthenticateWithOauth(code);

            return Ok(token);

        }
    }
}
