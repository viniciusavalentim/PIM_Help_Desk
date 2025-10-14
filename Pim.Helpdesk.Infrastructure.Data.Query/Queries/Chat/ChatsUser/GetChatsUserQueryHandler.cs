using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Chat.ChatsUser
{
    public class GetChatsUserQueryHandler : IRequestHandler<GetChatsUserQuery, GetChatsUserQueryResponse>
    {
        private readonly IChatRepository _chatRepository;

        public GetChatsUserQueryHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<GetChatsUserQueryResponse> Handle(GetChatsUserQuery request, CancellationToken cancellationToken)
        {
            var conversationsFromDb = await _chatRepository.GetConversationsByUserIdAsync(request.UserId);

            var conversationDtos = conversationsFromDb.Select(convo => new ConversationDto
            {
                Id = convo.Id,
                Title = convo.Title,
                CreatedAt = convo.CreatedAt,
                UpdatedAt = convo.UpdatedAt,
                Messages = convo.Messages.Select(msg => new ChatMessageDto(msg.Role, msg.Content)).ToList()
            }).ToList();

            return new GetChatsUserQueryResponse
            {
                Conversations = conversationDtos
            };
        }
    }
}
