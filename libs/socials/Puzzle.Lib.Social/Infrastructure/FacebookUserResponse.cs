namespace Puzzle.Lib.Social.Infrastructure;

public class FacebookUserInfoResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("picture")]
    public FacebookUserPicture Picture { get; set; }
}

public class FacebookUserPicture
{
    [JsonPropertyName("data")]
    public FacebookData Data { get; set; }
}

public class FacebookData
{
    [JsonPropertyName("height")]
    public long Height { get; set; }

    [JsonPropertyName("is_silhouette")]
    public bool IsSilhouette { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("width")]
    public long Width { get; set; }
}
