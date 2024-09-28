namespace Puzzle.Lib.Social.Infrastructure;

public sealed record FacebookUserInfoResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; init; }

    [JsonPropertyName("last_name")]
    public string LastName { get; init; }

    [JsonPropertyName("email")]
    public string Email { get; init; }

    [JsonPropertyName("picture")]
    public FacebookUserPicture Picture { get; init; }
}

public sealed record FacebookUserPicture
{
    [JsonPropertyName("data")]
    public FacebookData Data { get; init; }
}

public sealed record FacebookData
{
    [JsonPropertyName("height")]
    public long Height { get; init; }

    [JsonPropertyName("is_silhouette")]
    public bool IsSilhouette { get; init; }

    [JsonPropertyName("url")]
    public Uri Url { get; init; }

    [JsonPropertyName("width")]
    public long Width { get; init; }
}
