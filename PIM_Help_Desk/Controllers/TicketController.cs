using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pim.Helpdesk.Domain.Command.CreateTicketResponse;
using Pim.Helpdesk.Domain.Command.Ticket.AcceptTicket;
using Pim.Helpdesk.Domain.Command.Ticket.CreateTicket;
using Pim.Helpdesk.Domain.Command.Ticket.FinishTicket;
using Pim.Helpdesk.Domain.Command.Ticket.UpdateStatusTicket;
using Pim.Helpdesk.Infrastructure.Data.Query.Queries.Tickets.GetTickets;

namespace Pim.Helpdesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets([FromQuery] GetTicketsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTickeById(Guid id)
        {
            var query = new GetTicketQuery { TicketId = id };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("finish")]
        public async Task<IActionResult> FinishTickets([FromBody] FinishTicketCommand query)
        {
            var result = await _mediator.Send(query);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateTicketStatus([FromBody] UpdateStatusTicketCommand query)
        {
            var result = await _mediator.Send(query);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("response")]
        public async Task<IActionResult> CreateTicketResponse([FromBody] CreateTicketResponseCommand query)
        {
            var result = await _mediator.Send(query);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptTicket([FromBody] AcceptTicketCommand query)
        {
            var result = await _mediator.Send(query);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

    }
}
