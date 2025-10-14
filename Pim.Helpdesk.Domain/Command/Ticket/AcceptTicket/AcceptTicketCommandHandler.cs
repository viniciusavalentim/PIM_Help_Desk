using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;

namespace Pim.Helpdesk.Domain.Command.Ticket.AcceptTicket
{
    public class AcceptTicketCommandHandler : IRequestHandler<AcceptTicketCommand, AcceptTicketCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        public AcceptTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<AcceptTicketCommandResponse> Handle(AcceptTicketCommand request, CancellationToken cancellationToken)
        {
            AcceptTicketCommandResponse response = new AcceptTicketCommandResponse();
            try
            {
                var updatedTicket = await _ticketRepository.AcceptTicket(request.TicketId, request.UserId);
                if (updatedTicket)
                {
                    response.Message = "Ticket aceito com sucesso";
                    response.Success = true;
                }
                else
                {
                    response.Message = "Erro ao aceitar o ticket";
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
