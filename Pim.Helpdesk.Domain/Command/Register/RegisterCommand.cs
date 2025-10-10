using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Pim.Helpdesk.Domain.Command.Register
{
    public class RegisterCommand : IRequest<RegisterCommandResponse>
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        
        [Required]
        public required string Password { get; set; }
    }
}
