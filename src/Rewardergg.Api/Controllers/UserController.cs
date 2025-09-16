using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rewardergg.Api.Models;
using Rewardergg.Application.Interfaces;
using System.Security.Claims;

namespace Rewardergg.Api.Controllers
{
    [ApiController]
    [Route("~/api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IAuthWorkflowService _authWorkflowService;
        private readonly IEnrollmentService _enrollmentService; 

        public UserController(IAuthWorkflowService authWorkflowService)
        {
            _authWorkflowService = authWorkflowService;
        }

        [Authorize]
        [HttpPost]
        [Route("sync")]
        public async Task<IActionResult> SyncUserData(CancellationToken cancellationToken)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub");
            if (userIdClaim == null)
                return Unauthorized();

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized();

            await _authWorkflowService.SyncUserDataAsync(userId, cancellationToken);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("enroll")]
        public async Task<IActionResult> EnrollInEvent([FromBody] UserEnrollmentRequest request, CancellationToken cancellationToken)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized();

            var result = await _enrollmentService.EnrollUserInEventAsync(userId, request.EventId, cancellationToken);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}