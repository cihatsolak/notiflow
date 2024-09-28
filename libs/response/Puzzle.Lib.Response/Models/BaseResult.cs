namespace Puzzle.Lib.Response.Models;

/// <summary>
/// Represents the base result for a response.
/// </summary>
public abstract record BaseResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the request was successful or not.
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Gets a value indicating whether the request was unsuccessful.
    /// </summary>
    [JsonIgnore]
    public bool IsFailed => !IsSuccess;
    
    /// <summary>
    /// Gets or sets the status code of the response.
    /// </summary>
    [JsonIgnore]
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the status message of the response.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the URL as created for the response.
    /// </summary>
    [JsonIgnore]
    public string UrlAsCreated { get; set; }

    /// <summary>
    /// Gets or sets the errors that occurred during the request.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<string> Errors { get; init; }
}
