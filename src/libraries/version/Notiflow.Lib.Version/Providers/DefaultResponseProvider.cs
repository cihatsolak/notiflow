namespace Notiflow.Lib.Version.Providers
{
    public sealed class DefaultResponseProvider : DefaultErrorResponseProvider
    {
        private readonly int _statusCode;
        private readonly string _errorCode;
        private readonly string _errorMessage;

        public DefaultResponseProvider()
        {
            _statusCode = StatusCodes.Status400BadRequest;
            _errorCode = StatusCodes.Status400BadRequest.ToString();
        }

        public DefaultResponseProvider(string errorMessage) : this()
        {
            _errorMessage = errorMessage;
        }

        public DefaultResponseProvider(int statusCode, string errorMessage)
        {
            _statusCode = statusCode;
            _errorCode = statusCode.ToString();
            _errorMessage = errorMessage;
        }

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
