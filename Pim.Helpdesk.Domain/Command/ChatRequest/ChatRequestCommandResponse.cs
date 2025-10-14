using System.Text.Json.Serialization;

namespace Pim.Helpdesk.Domain.Command.ChatRequest
{
    public class ChatRequestCommandResponse
    {
        public Guid ConversationId { get; set; }
        public List<Candidate> Candidates { get; set; } = new();

        [JsonIgnore]
        public string? PrimaryReply => Candidates.FirstOrDefault()?
                                                  .Content?
                                                  .Parts?
                                                  .FirstOrDefault()?
                                                  .ToString() ?? "Não foi possível obter uma resposta.";
        [JsonIgnore]
        public bool IsSuccess => Candidates.Any() && Candidates.First().Content?.Parts?.Any() == true;
    }
}