using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pim.Helpdesk.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Pim.Helpdesk.Domain.Command.ChatRequest
{
    public class ChatRequestCommandHandler : IRequestHandler<ChatRequestCommand, ChatRequestCommandResponse>
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly IChatRepository _chatRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ChatRequestCommandHandler> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ChatRequestCommandHandler(
            IConfiguration configuration,
            IChatRepository chatRepository,
            IHttpClientFactory httpClientFactory,
            ILogger<ChatRequestCommandHandler> logger
        )
        {
            _chatRepository = chatRepository;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["Gemini:ApiKey"] ?? throw new InvalidOperationException("A chave da API do Gemini (Gemini:ApiKey) não foi encontrada na configuração.");
            var model = "gemini-2.5-flash";
            _apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={_apiKey}";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ChatRequestCommandResponse> Handle(ChatRequestCommand request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("GeminiApiClient");

            try
            {
                var payload = CreatePayloadFromRequest(request);
                var response = await httpClient.PostAsJsonAsync(_apiUrl, payload, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return await HandleApiErrorAsync(response, cancellationToken);
                }

                var geminiApiResponse = await response.Content.ReadFromJsonAsync<GeminiApiResponse>(_jsonSerializerOptions, cancellationToken);


                if (geminiApiResponse != null && geminiApiResponse.Candidates.Any())
                {
                    var newModelReply = geminiApiResponse.Candidates.First()?.Content?.Parts?.First()?.Text;

                    if (!string.IsNullOrEmpty(newModelReply))
                    {
                        var fullHistory = new List<ChatMessageDto>(request.History);
                        fullHistory.Add(new ChatMessageDto("user", request.Message));
                        fullHistory.Add(new ChatMessageDto("assistant", newModelReply));

                        var savedConversation = await _chatRepository.StoreConversationHistoryAsync(
                           request.ConversationId,
                           request.UserId,
                           fullHistory
                        );

                        return new ChatRequestCommandResponse
                        {
                            Candidates = geminiApiResponse.Candidates,
                            ConversationId = savedConversation.Id
                        };
                    }
                }

                return new ChatRequestCommandResponse
                {
                    Candidates = geminiApiResponse?.Candidates ?? new List<Candidate>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu uma exceção inesperada ao chamar a API do Gemini.");
                return new ChatRequestCommandResponse();
            }
        }

        private GeminiRequestPayload CreatePayloadFromRequest(ChatRequestCommand request)
        {
            var contentList = new List<GeminiContent>();

            foreach (var message in request.History)
            {
                if (string.IsNullOrEmpty(message.Role) || string.IsNullOrEmpty(message.Content)) continue;
                var role = message.Role.Equals("assistant", StringComparison.OrdinalIgnoreCase) ? "model" : "user";
                contentList.Add(new GeminiContent(role, new[] { new GeminiPart(message.Content) }));
            }

            var systemInstruction = @"
                Você é Sabiá, um assistente virtual de help desk.Sua principal função é
                ajudar os usuários a resolverem suas dúvidas de forma clara e direta.

                REGRAS IMPORTANTES:
                1.  **Seja Conciso**: Forneça respostas curtas e objetivas, com no máximo
                    três parágrafos.Vá direto ao ponto.
                2.  **Foco no Help Desk**: Responda apenas a perguntas relacionadas a suporte,
                    dúvidas sobre o sistema, ou problemas técnicos.Se o usuário perguntar
                    sobre outros assuntos (como o tempo, filosofia, etc.), recuse educadamente
                    e redirecione para o tema de suporte.
                3.  **Tom Amigável e Profissional**: Mantenha um tom prestativo e cordial.
                4.  **Não invente informações**: Se não souber a resposta, diga que não
                    possui essa informação e oriente o usuário a procurar um canal de
                    suporte humano.

                Mensagem de ajuda do usuário:
            " + $"  {request.Message}";

            contentList.Add(new GeminiContent("user", new[] { new GeminiPart(systemInstruction) }));

            return new GeminiRequestPayload(contentList.ToArray());
        }
        private async Task<ChatRequestCommandResponse> HandleApiErrorAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Erro na chamada à API do Gemini. Status: {StatusCode}, Resposta: {ErrorContent}", response.StatusCode, errorContent);
            return new ChatRequestCommandResponse();
        }
    }

    public record GeminiRequestPayload(GeminiContent[] contents);
    public record GeminiContent(string role, GeminiPart[] parts);
    public record GeminiPart(
        [property: JsonPropertyName("text")] string Text
    );
    public record Candidate(
       [property: JsonPropertyName("content")] GeminiResponseContent Content
    );
    public record GeminiApiResponse(
        [property: JsonPropertyName("candidates")] List<Candidate> Candidates
    );
    public record GeminiResponseContent(
        [property: JsonPropertyName("parts")] List<GeminiPart> Parts,
        [property: JsonPropertyName("role")] string Role
    );
}