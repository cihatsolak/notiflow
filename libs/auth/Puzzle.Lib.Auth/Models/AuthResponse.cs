namespace Puzzle.Lib.Auth.Models;

internal sealed class AuthResponse
{
    /// <summary>
    /// Gets or sets the status message of the response.
    /// </summary>
    [JsonRequired]
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the errors that occurred during the request.
    /// </summary>
    public string Error { get; set; }
}
