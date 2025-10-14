using MediatR;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using PIM_Help_Desk.Models;

namespace Pim.Helpdesk.Domain.Command.Ticket.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, CreateTicketCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<CreateTicketCommandResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            CreateTicketCommandResponse response = new CreateTicketCommandResponse();
            try
            {
                var createdTicket = await _ticketRepository.CreateTicket(request.RequesterId, request.Title, request.Description, 1, request.Priority, request.Category);

                if (createdTicket)
                {
                    response.Message = "Chamado criado com sucesso";
                    response.Success = true;
                }
                else
                {
                    response.Message = "Erro ao criar chamado";
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
