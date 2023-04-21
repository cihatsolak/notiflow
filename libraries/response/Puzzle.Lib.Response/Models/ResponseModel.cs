namespace Puzzle.Lib.Response.Models
{
    /// <summary>
    /// Represents a response model that can hold data, status code, success flag, status message and errors.
    /// </summary>
    /// <typeparam name="TData">The type of the data that the response model holds.</typeparam>
    public sealed record ResponseModel<TData>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the request was successful or not.
        /// </summary>
        [JsonIgnore]
        public bool Succeeded { get; init; }

        /// <summary>
        /// Gets or sets the data that the response model holds.
        /// </summary>
        public TData Data { get; init; }

        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        [JsonRequired]
        public int Code { get; init; }

        /// <summary>
        /// Gets or sets the status message of the response.
        /// </summary>
        [JsonRequired]
        public string Message { get; init; }

        /// <summary>
        /// Gets or sets the errors that occurred during the request.
        /// </summary>
        public IEnumerable<string> Errors { get; init; }


        /// <summary>
        /// Creates a success response with the specified status code.
        /// </summary>
        /// <param name="code">The status code of the response.</param>
        /// <returns>A success response with the specified status code.</returns>
        public static ResponseModel<TData> Success(int code)
        {
            return new ResponseModel<TData>
            {
                Code = code,
                Succeeded = true
            };
        }

        /// <summary>
        /// Creates a success response with the specified data.
        /// </summary>
        /// <param name="data">The data that the response model holds.</param>
        /// <returns>A success response with the specified data.</returns>
        public static ResponseModel<TData> Success(TData data)
        {
            return new ResponseModel<TData>
            {
                Data = data,
                Code = 9001,
                Succeeded = true
            };
        }

        /// <summary>
        /// Creates a success response with the specified status code and data.
        /// </summary>
        /// <param name="code">The status code of the response.</param>
        /// <param name="data">The data that the response model holds.</param>
        /// <returns>A success response with the specified status code and data.</returns>
        public static ResponseModel<TData> Success(int code, TData data)
        {
            return new ResponseModel<TData>
            {
                Data = data,
                Code = code,
                Succeeded = true
            };
        }

        /// <summary>
        /// Creates a success response with the specified status code, status message and data.
        /// </summary>
        /// <param name="code">The status code of the response.</param>
        /// <param name="message">The status message of the response.</param>
        /// <param name="data">The data that the response model holds.</param>
        /// <returns>A success response with the specified status code, status message and data.</returns>
        public static ResponseModel<TData> Success(int code, string message, TData data)
        {
            return new ResponseModel<TData>
            {
                Data = data,
                Code = code,
                Succeeded = true,
                Message = message
            };
        }

        /// <summary>
        /// Creates a failed response with the specified status code.
        /// </summary>
        /// <param name="code">The HTTP status code.</param>
        /// <returns>A failed response with the specified status code.</returns>
        public static ResponseModel<TData> Fail(int code)
        {
            return new ResponseModel<TData>
            {
                Code = code
            };
        }

        /// <summary>
        /// Returns a failure response model with a single error message.
        /// </summary>
        /// <param name="code">The HTTP status code for the response.</param>
        /// <param name="error">The error message for the response.</param>
        /// <returns>A response model indicating failure with the specified status code and error message.</returns>
        public static ResponseModel<TData> Fail(int code, string error)
        {
            return new ResponseModel<TData>
            {
                Errors = new List<string>() { error },
                Code = code
            };
        }

        /// <summary>
        /// Returns a failure response model with a list of error messages.
        /// </summary>
        /// <param name="code">The HTTP status code for the response.</param>
        /// <param name="errors">The list of error messages for the response.</param>
        /// <returns>A response model indicating failure with the specified status code and error messages.</returns>
        public static ResponseModel<TData> Fail(int code, IEnumerable<string> errors)
        {
            return new ResponseModel<TData>
            {
                Errors = errors,
                Code = code
            };
        }
    }
}
