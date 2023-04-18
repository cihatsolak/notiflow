﻿namespace Notiflow.Backoffice.Application.Models;

public sealed record FirebaseResponse
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

    public bool Succeeded => Success == 1;
}

public sealed record FirebaseResult
{
    [JsonPropertyName("message_id")]
    public string MessageId { get; init; }

    [JsonPropertyName("error")]
    public string ErrorMessage { get; init; }
}