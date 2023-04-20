namespace Notiflow.Backoffice.Application.Models;

public sealed record FirebaseResult
{
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; }

    [JsonPropertyName("error")]
    public string ErrorMessage { get; init; }
}