using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;

namespace Pim.Helpdesk.Domain.Command.Ticket.FinishTicket
{
    public class FinishTicketCommandHandler : IRequestHandler<FinishTicketCommand, FinishTicketCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        public FinishTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<FinishTicketCommandResponse> Handle(FinishTicketCommand request, CancellationToken cancellationToken)
        {
            FinishTicketCommandResponse response = new FinishTicketCommandResponse();
            try
            {
                var finishedTicket = await _ticketRepository.FinishTicket(request.TicketId, request.UserId);
                if (finishedTicket)
                {
                    response.Message = "Chamado finalizado com sucesso";
                    response.Success = true;
                }
                else
                {
                    response.Message = "Erro ao finalizar chamado";
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
