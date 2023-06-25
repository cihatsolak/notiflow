namespace Puzzle.Lib.Host.Models;

internal sealed class ErrorHandlerResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the request was successful or not.
    /// </summary>
    public bool Succeeded { get; init; }

    /// <summary>
    /// Gets or sets the status code of the response.
    /// </summary>
    public int Code { get; init; }

    /// <summary>
    /// Gets or sets the status message of the response.
    /// </summary>
    public string Message { get; init; }
}
