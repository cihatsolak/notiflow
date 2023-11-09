namespace Puzzle.Lib.Response.Models;

/// <summary>
/// Represents a response model that can hold data, status statusCode, success flag, status message and errors.
/// </summary>
/// <typeparam name="TData">The type of the data that the response model holds.</typeparam>
public record Result<TData>
{
    /// <summary>
    /// Gets or sets a value indicating whether the request was successful or not.
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess { get; init; }

    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets or sets the data that the response model holds.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public TData Data { get; init; }

    /// <summary>
    /// Gets or sets the status statusCode of the response.
    /// </summary>
    [JsonIgnore]
    public int StatusCode { get; set; }

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
    /// Creates a success response with the specified status statusCode.
    /// </summary>
    /// <param name="statusCode">The status statusCode of the response.</param>
    /// <returns>A success response with the specified status statusCode.</returns>
    public static Result<TData> Success(int statusCode)
    {
        return new Result<TData>
        {
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Creates a success response with the specified data. | StatusCodes.Status200OK
    /// </summary>
    /// <param name="data">The data that the response model holds.</param>
    /// <returns>A success response with the specified data.</returns>
    public static Result<TData> Success(TData data)
    {
        return new Result<TData>
        {
            Data = data,
            StatusCode = StatusCodes.Status200OK,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Creates a success response with the specified status statusCode and data.
    /// </summary>
    /// <param name="statusCode">The status statusCode of the response.</param>
    /// <param name="message">The status message of the response.</param>
    /// <returns>A success response with the specified status statusCode and data.</returns>
    public static Result<TData> Success(int statusCode, string message)
    {
        return new Result<TData>
        {
            StatusCode = statusCode,
            IsSuccess = true,
            Message = message
        };
    }

    /// <summary>
    /// Creates a success response with the specified status statusCode and data.
    /// </summary>
    /// <param name="statusCode">The status statusCode of the response.</param>
    /// <param name="data">The data that the response model holds.</param>
    /// <returns>A success response with the specified status statusCode and data.</returns>
    public static Result<TData> Success(int statusCode, TData data)
    {
        return new Result<TData>
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Creates a success response with the specified status statusCode, status message and data.
    /// </summary>
    /// <param name="statusCode">The status statusCode of the response.</param>
    /// <param name="message">The status message of the response.</param>
    /// <param name="data">The data that the response model holds.</param>
    /// <returns>A success response with the specified status statusCode, status message and data.</returns>
    public static Result<TData> Success(int statusCode, string message, TData data)
    {
        return new Result<TData>
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccess = true,
            Message = message
        };
    }

    /// <summary>
    /// Creates a failed response with the specified status statusCode.
    /// </summary>
    /// <param name="statusCode">The HTTP status statusCode.</param>
    /// <returns>A failed response with the specified status statusCode.</returns>
    public static Result<TData> Failure(int statusCode)
    {
        return new Result<TData>
        {
            StatusCode = statusCode
        };
    }

    /// <summary>
    /// Returns a failure response model with a single error message.
    /// </summary>
    /// <param name="statusCode">The HTTP status statusCode for the response.</param>
    /// <param name="message">The error message for the response.</param>
    /// <returns>A response model indicating failure with the specified status statusCode and error message.</returns>
    public static Result<TData> Failure(int statusCode, string message)
    {
        return new Result<TData>
        {
            Message = message,
            Errors = new List<string>() { message },
            StatusCode = statusCode
        };
    }

    /// <summary>
    /// Returns a failure response model with a list of error messages.
    /// </summary>
    /// <param name="statusCode">The HTTP status statusCode for the response.</param>
    /// <param name="errors">The list of error messages for the response.</param>
    /// <returns>A response model indicating failure with the specified status statusCode and error messages.</returns>
    public static Result<TData> Failure(int statusCode, IEnumerable<string> errors)
    {
        return new Result<TData>
        {
            Errors = errors,
            StatusCode = statusCode
        };
    }
}
