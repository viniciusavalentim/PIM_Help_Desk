using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;

namespace Pim.Helpdesk.Domain.Command.Ticket.UpdateStatusTicket
{
    public class UpdateStatusTicketCommandHandler : IRequestHandler<UpdateStatusTicketCommand, UpdateStatusTicketCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        public UpdateStatusTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<UpdateStatusTicketCommandResponse> Handle(UpdateStatusTicketCommand request, CancellationToken cancellationToken)
        {
            UpdateStatusTicketCommandResponse response = new UpdateStatusTicketCommandResponse();
            try
            {
                var updatedTicket = await _ticketRepository.UpdateStatusTicket(request.TicketId, request.Status, request.UserId);
                if (updatedTicket)
                {
                    response.Message = "Status do chamado atualizado com sucesso";
                    response.Success = true;
                }
                else
                {
                    response.Message = "Erro ao atualizar status do chamado";
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
