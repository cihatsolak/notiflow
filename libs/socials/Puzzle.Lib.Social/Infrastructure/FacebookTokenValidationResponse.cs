namespace Puzzle.Lib.Social.Infrastructure;

internal sealed record FacebookTokenValidationResponse
{
    [JsonPropertyName("data")]
    public FacebookTokenValidationData Data { get; init; }
}

public sealed record FacebookTokenValidationData
{
    [JsonPropertyName("app_id")]
    public string AppId { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("application")]
    public string Application { get; init; }

    [JsonPropertyName("data_access_expires_at")]
    public long DataAccessExpiresAt { get; init; }

    [JsonPropertyName("expires_at")]
    public long ExpiresAt { get; init; }

    [JsonPropertyName("is_valid")]
    public bool IsValid { get; init; }

    [JsonPropertyName("metadata")]
    public FacebookMetadata Metadata { get; init; }

    [JsonPropertyName("scopes")]
    public string[] Scopes { get; init; }

    [JsonPropertyName("user_id")]
    public string UserId { get; init; }
}

public sealed record FacebookMetadata
{
    [JsonPropertyName("auth_type")]
    public string AuthType { get; init; }
}
