using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pim.Helpdesk.Domain.Command.Login;
using Pim.Helpdesk.Domain.Command.Register;

namespace PIM_Help_Desk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success) return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        //[HttpGet("getAllUsers")]
        //public async Task<GetUsersQueryResponse> GetUsers()
        //{
        //    return await _mediator.Send(new GetUsersQuery());
        //}
    }
}
