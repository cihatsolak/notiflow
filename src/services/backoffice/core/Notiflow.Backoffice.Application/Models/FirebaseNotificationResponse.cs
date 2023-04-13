namespace Notiflow.Backoffice.Application.Models;

public sealed record FirebaseNotificationResponse
{
    [JsonPropertyName("multicast_id")]
    public double MulticastId { get; init; }

    [JsonPropertyName("success")]
    public int Success { get; init; }

    [JsonPropertyName("failure")]
    public int Failure { get; init; }

    [JsonPropertyName("canonical_ids")]
    public int CanonicalIds { get; init; }

    [JsonPropertyName("results")]
    public List<FirebaseResult> Results { get; init; }

    public bool IsSuccess => Success == 1;

    public string MessageId => Results.FirstOrDefault()?.MessageId;
    public string ErrorMessage => Results.FirstOrDefault()?.ErrorMessage;
}

public sealed record FirebaseResult
{
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; }

    [JsonPropertyName("error")]
    public string ErrorMessage { get; init; }
}
