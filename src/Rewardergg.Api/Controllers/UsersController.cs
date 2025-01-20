using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rewardergg.Application.Commands;

namespace Rewardergg.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return Ok(userId); ;
        }

    }
}
