using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pim.Helpdesk.Infrastructure.Data.Query.Queries.Users.GetUser;

namespace Pim.Helpdesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] GetUserQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
