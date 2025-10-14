using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pim.Helpdesk.Domain.Command.ChatRequest;
using Pim.Helpdesk.Infrastructure.Data.Query.Queries.Chat.ChatsUser;

namespace Pim.Helpdesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OpenAIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ChatRequestCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostMessage([FromBody] ChatRequestCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("A mensagem não pode ser vazia.");
            }

            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{userId:guid}")]
        [ProducesResponseType(typeof(GetChatsUserQueryResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetChatsByUser([FromRoute] Guid userId)
        {
            var query = new GetChatsUserQuery(userId);
            var result = await _mediator.Send(query);

            if (result == null || !result.Conversations.Any())
            {
                return NotFound("Nenhuma conversa encontrada para este usuário.");
            }

            return Ok(result);
        }

    }
}
