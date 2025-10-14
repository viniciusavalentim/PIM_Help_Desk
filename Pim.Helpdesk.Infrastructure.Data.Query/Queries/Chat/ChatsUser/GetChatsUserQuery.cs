using MediatR;

namespace Pim.Helpdesk.Infrastructure.Data.Query.Queries.Chat.ChatsUser
{
    public class GetChatsUserQuery : IRequest<GetChatsUserQueryResponse>
    {
        public Guid UserId { get; }

        public GetChatsUserQuery(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("O ID do usuário não pode ser vazio.", nameof(userId));
            }
            UserId = userId;
        }
    }
}
