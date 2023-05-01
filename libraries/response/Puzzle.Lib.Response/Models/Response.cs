namespace Puzzle.Lib.Response.Models
{
    /// <summary>
    /// Represents the response object returned from API calls.
    /// </summary>
    public sealed record Response
    {
        /// <summary>
        /// Gets or sets a value indicating whether the response operation was successful.
        /// </summary>
        [JsonIgnore]
        public bool Succeeded { get; init; }

        /// <summary>
        /// Gets or sets the code of the response.
        /// </summary>
        [JsonRequired]
        public int Code { get; init; }

        /// <summary>
        /// Gets or sets the message of the response.
        /// </summary>
        [JsonRequired]
        public string Message { get; init; }

        /// <summary>
        /// Gets or sets the collection of errors associated with the response.
        /// </summary>
        public IEnumerable<string> Errors { get; init; }


        /// <summary>
        /// Creates a successful response object with the specified code.
        /// </summary>
        /// <param name="code">The code of the response.</param>
        /// <returns>A new instance of the <see cref="Response"/> class.</returns>
        public static Response Success(int code)
        {
            return new Response
            {
                Code = code,
                Succeeded = true
            };
        }

        /// <summary>
        /// Creates a successful response object with the specified code and message.
        /// </summary>
        /// <param name="code">The code of the response.</param>
        /// <param name="message">The message of the response.</param>
        /// <returns>A new instance of the <see cref="Response"/> class.</returns>
        public static Response Success(int code, string message)
        {
            return new Response
            {
                Code = code,
                Succeeded = true,
                Message = message
            };
        }

        /// <summary>
        /// Creates a failed response object with the specified code.
        /// </summary>
        /// <param name="code">The code of the response.</param>
        /// <returns>A new instance of the <see cref="Response"/> class.</returns>
        public static Response Fail(int code)
        {
            return new Response
            {
                Code = code
            };
        }

        /// <summary>
        /// Creates a failed response object with the specified code and error message.
        /// </summary>
        /// <param name="code">The code of the response.</param>
        /// <param name="error">The error message of the response.</param>
        /// <returns>A new instance of the <see cref="Response"/> class.</returns>
        public static Response Fail(int code, string error)
        {
            return new Response
            {
                Errors = new List<string>() { error },
                Code = code
            };
        }

        /// <summary>
        /// Creates a failed response object with the specified code and collection of error messages.
        /// </summary>
        /// <param name="code">The code of the response.</param>
        /// <param name="errors">The collection of error messages of the response.</param>
        /// <returns>A new instance of the <see cref="Response"/> class.</returns>
        public static Response Fail(int code, IEnumerable<string> errors)
        {
            return new Response
            {
                Errors = errors,
                Code = code
            };
        }
    }
}
