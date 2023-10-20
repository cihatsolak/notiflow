namespace Puzzle.Lib.Response.Models;

/// <summary>
/// Represents a response model that can hold data, status httpStatusCode, success flag, status message and errors.
/// </summary>
/// <typeparam name="TData">The type of the data that the response model holds.</typeparam>
public record Result<TData>
{
    /// <summary>
    /// Gets or sets a value indicating whether the request was successful or not.
    /// </summary>
    [JsonIgnore]
    public bool Succeeded { get; init; }

    /// <summary>
    /// Gets or sets the data that the response model holds.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData Data { get; init; }

    /// <summary>
    /// Gets or sets the status httpStatusCode of the response.
    /// </summary>
    [JsonIgnore]
    public int HttpStatusCode { get; set; }

    /// <summary>
    /// Gets or sets the status message of the response.
    /// </summary>
    [JsonRequired]
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the errors that occurred during the request.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<string> Errors { get; init; }


    /// <summary>
    /// Creates a success response with the specified status httpStatusCode.
    /// </summary>
    /// <param name="httpStatusCode">The status httpStatusCode of the response.</param>
    /// <returns>A success response with the specified status httpStatusCode.</returns>
    public static Result<TData> Success(int httpStatusCode)
    {
        return new Result<TData>
        {
            HttpStatusCode = httpStatusCode,
            Succeeded = true
        };
    }

    /// <summary>
    /// Creates a success response with the specified data.
    /// </summary>
    /// <param name="data">The data that the response model holds.</param>
    /// <returns>A success response with the specified data.</returns>
    public static Result<TData> Success(TData data)
    {
        return new Result<TData>
        {
            Data = data,
            HttpStatusCode = 9001,
            Succeeded = true
        };
    }

    /// <summary>
    /// Creates a success response with the specified status httpStatusCode and data.
    /// </summary>
    /// <param name="httpStatusCode">The status httpStatusCode of the response.</param>
    /// <param name="data">The data that the response model holds.</param>
    /// <returns>A success response with the specified status httpStatusCode and data.</returns>
    public static Result<TData> Success(int httpStatusCode, TData data)
    {
        return new Result<TData>
        {
            Data = data,
            HttpStatusCode = httpStatusCode,
            Succeeded = true
        };
    }

    /// <summary>
    /// Creates a success response with the specified status httpStatusCode, status message and data.
    /// </summary>
    /// <param name="httpStatusCode">The status httpStatusCode of the response.</param>
    /// <param name="message">The status message of the response.</param>
    /// <param name="data">The data that the response model holds.</param>
    /// <returns>A success response with the specified status httpStatusCode, status message and data.</returns>
    public static Result<TData> Success(int httpStatusCode, string message, TData data)
    {
        return new Result<TData>
        {
            Data = data,
            HttpStatusCode = httpStatusCode,
            Succeeded = true,
            Message = message
        };
    }

    /// <summary>
    /// Creates a failed response with the specified status httpStatusCode.
    /// </summary>
    /// <param name="httpStatusCode">The HTTP status httpStatusCode.</param>
    /// <returns>A failed response with the specified status httpStatusCode.</returns>
    public static Result<TData> Failure(int httpStatusCode)
    {
        return new Result<TData>
        {
            HttpStatusCode = httpStatusCode
        };
    }

    /// <summary>
    /// Returns a failure response model with a single error message.
    /// </summary>
    /// <param name="httpStatusCode">The HTTP status httpStatusCode for the response.</param>
    /// <param name="error">The error message for the response.</param>
    /// <returns>A response model indicating failure with the specified status httpStatusCode and error message.</returns>
    public static Result<TData> Failure(int httpStatusCode, string error)
    {
        return new Result<TData>
        {
            Errors = new List<string>() { error },
            HttpStatusCode = httpStatusCode
        };
    }

    /// <summary>
    /// Returns a failure response model with a list of error messages.
    /// </summary>
    /// <param name="httpStatusCode">The HTTP status httpStatusCode for the response.</param>
    /// <param name="errors">The list of error messages for the response.</param>
    /// <returns>A response model indicating failure with the specified status httpStatusCode and error messages.</returns>
    public static Result<TData> Failure(int httpStatusCode, IEnumerable<string> errors)
    {
        return new Result<TData>
        {
            Errors = errors,
            HttpStatusCode = httpStatusCode
        };
    }
}
