using Microsoft.AspNetCore.Mvc;
using Rewardergg.Application.Interfaces;

namespace Rewardergg.Api.Controllers
{
    [ApiController]
    [Route("~/api/v1/login")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthWorkflowService _authWorkflowService;

        public LoginController(IAuthWorkflowService authWorkflowService)
        {
            _authWorkflowService = authWorkflowService;
        }

        [HttpGet]
        [Route("oauth")]
        public async Task<IActionResult> OauthLogin(string code, CancellationToken cancellationToken)
        {
 
            var jwt = await _authWorkflowService.LoginAsync(code, cancellationToken);

            return Ok(jwt);
        }
    }
}
