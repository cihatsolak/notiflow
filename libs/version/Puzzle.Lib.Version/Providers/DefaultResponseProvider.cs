namespace Puzzle.Lib.Version.Providers
{
    /// <summary>
    /// Default response provider that can be used to customize the error response returned by the API.
    /// </summary>
    public sealed class DefaultResponseProvider : DefaultErrorResponseProvider
    {
        private readonly int _statusCode;
        private readonly string _errorCode;
        private readonly string _errorMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultResponseProvider"/> class with default status code and error code.
        /// </summary>
        public DefaultResponseProvider()
        {
            _statusCode = StatusCodes.Status400BadRequest;
            _errorCode = StatusCodes.Status400BadRequest.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultResponseProvider"/> class with a custom error message.
        /// </summary>
        /// <param name="errorMessage">The custom error message to be included in the response.</param>
        public DefaultResponseProvider(string errorMessage) : this()
        {
            _errorMessage = errorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultResponseProvider"/> class with custom status code and error message.
        /// </summary>
        /// <param name="statusCode">The custom status code to be included in the response.</param>
        /// <param name="errorMessage">The custom error message to be included in the response.</param>
        public DefaultResponseProvider(int statusCode, string errorMessage)
        {
            _statusCode = statusCode;
            _errorCode = statusCode.ToString();
            _errorMessage = errorMessage;
        }

        /// <summary>
        /// Creates a customized error response for the specified <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The error response context.</param>
        /// <returns>A customized error response.</returns>
        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            if (context.ErrorCode.Equals("UnsupportedApiVersion", StringComparison.CurrentCultureIgnoreCase))
            {
                context = new ErrorResponseContext(
                       context.Request,
                       _statusCode,
                       _errorCode,
                      _errorMessage,
                       context.MessageDetail);
            }

            return base.CreateResponse(context);
        }
    }
}
