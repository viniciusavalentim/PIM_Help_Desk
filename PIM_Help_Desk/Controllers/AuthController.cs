using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pim.Helpdesk.Domain.Command.Login;

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

        //[HttpPost("register")]
        //public async Task<IActionResult> Register(RegisterUserCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    if (!result.Success) return BadRequest(result.Message);

        //    return Ok(result.Message);
        //}

        //[HttpGet("getAllUsers")]
        //public async Task<GetUsersQueryResponse> GetUsers()
        //{
        //    return await _mediator.Send(new GetUsersQuery());
        //}

        //[HttpPost("register")]
        //public async Task<ActionResult<User>> Register(RegisterDto request)
        //{
        //    //var register = await _authService.Register(request);
        //    //if (register.Status)
        //    //{
        //    //    return Ok(register);
        //    //}

        //    //return BadRequest(register);
        //}

        //[HttpPost("login")]
        //public async Task<ActionResult<string>> Login(LoginDto request)
        //{
        //    var response = await _mediator.Send().Login(request);
        //    if (response.Status)
        //    {
        //        return Ok(response);
        //    }

        //    return BadRequest(response);
        //}
    }
}
