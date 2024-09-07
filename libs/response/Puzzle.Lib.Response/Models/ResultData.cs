namespace Puzzle.Lib.Response.Models;

/// <summary>
/// Represents a response model that can hold data, status statusCode, success flag, status message and errors.
/// </summary>
/// <typeparam name="TData">The type of the data that the response model holds.</typeparam>
public sealed record Result<TData> : BaseResult
{
    /// <summary>
    /// Gets or sets the data that the response model holds.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public TData Data { get; init; }

    /// <summary>
    /// Creates a Result with a 200 OK status code and a custom result code.
    /// </summary>
    /// <param name="message">The status message of the response.</param>
    /// <returns>A Result with a 200 OK status code and the specified result code.</returns>
    public static Result<TData> Status200OK(string message)
    {
        return new Result<TData>
        {
            StatusCode = StatusCodes.Status200OK,
            Message = message,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Creates a Result with a 200 OK status code, a custom result code, and associated data.
    /// </summary>
    /// <param name="message">The status message of the response.</param>
    /// <param name="data">The data associated with the result.</param>
    /// <returns>A Result with a 200 OK status code, the specified result code, and associated data.</returns>
    public static Result<TData> Status200OK(string message, TData data)
    {
        return new Result<TData>
        {
            Data = data,
            StatusCode = StatusCodes.Status200OK,
            Message = message,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Creates a Result with a 201 Created status code, a custom result code, and associated data.
    /// </summary>
    /// <param name="message">The status message of the response.</param>
    /// <param name="data">The data associated with the result.</param>
    /// <returns>A Result with a 201 Created status code, the specified result code, and associated data.</returns>
    public static Result<TData> Status201Created(string message, TData data)
    {
        return new Result<TData>
        {
            Data = data,
            StatusCode = StatusCodes.Status201Created,
            Message = message,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Creates a Result with a 202 Accepted status code and a custom result code.
    /// </summary>
    /// <param name="message">The status message of the response.</param>
    /// <returns>A Result with a 202 Accepted status code and the specified result code.</returns>
    public static Result<TData> Status202Accepted(string message)
    {
        return new Result<TData>
        {
            StatusCode = StatusCodes.Status202Accepted,
            Message = message,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Creates a Result with a 204 No Content status code and a custom result code.
    /// </summary>
    /// <returns>A Result with a 204 No Content status code and the specified result code.</returns>
    public static Result<TData> Status204NoContent()
    {
        return new Result<TData>
        {
            StatusCode = StatusCodes.Status204NoContent,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Creates a Result with a 400 Bad Request status code and a custom result code.
    /// </summary>
    /// <param name="message">The status message of the response.</param>
    /// <returns>A Result with a 400 Bad Request status code and the specified result code.</returns>
    public static Result<TData> Status400BadRequest(string message)
    {
        return new Result<TData>
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = message
        };
    }

    /// <summary>
    /// Creates a Result with a 404 Not Found status code and a custom result code.
    /// </summary>
    /// <param name="message">The status message of the response.</param>
    /// <returns>A Result with a 404 Not Found status code and the specified result code.</returns>
    public static Result<TData> Status404NotFound(string message)
    {
        return new Result<TData>
        {
            StatusCode = StatusCodes.Status404NotFound,
            Message = message
        };
    }

    /// <summary>
    /// Creates a Result with a 500 Internal Server Error status code and a custom result code.
    /// </summary>
    /// <param name="resultCode">The custom result code.</param>
    /// <returns>A Result with a 500 Internal Server Error status code and the specified result code.</returns>
    public static Result<TData> Status500InternalServerError(string message)
    {
        return new Result<TData>
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = message
        };
    }

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
            Message = message,
            IsSuccess = true
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
            Message = message,
            IsSuccess = true
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
            StatusCode = statusCode,
            Message = message
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
