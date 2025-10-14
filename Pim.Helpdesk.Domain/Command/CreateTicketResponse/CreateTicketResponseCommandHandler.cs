using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;

namespace Pim.Helpdesk.Domain.Command.CreateTicketResponse
{
    public class CreateTicketResponseCommandHandler : IRequestHandler<CreateTicketResponseCommand, CreateTicketResponseCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        public CreateTicketResponseCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<CreateTicketResponseCommandResponse> Handle(CreateTicketResponseCommand request, CancellationToken cancellationToken)
        {
            CreateTicketResponseCommandResponse response = new CreateTicketResponseCommandResponse();
            try
            {
                var updatedTicket = await _ticketRepository.CreateTicketResponse(request.TicketId, request.UserId, request.Title);
                if (updatedTicket)
                {
                    response.Message = "Resposta criada com sucesso";
                    response.Success = true;
                }
                else
                {
                    response.Message = "Erro ao criar resposta do ticket";
                    response.Success = false;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
    }
}
