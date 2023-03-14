namespace Puzzle.Lib.Version.Providers
{
    /// <summary>
    /// Provides customized error response for API versioning in case of errors.
    /// Overrides the base implementation of CreateResponse method to return a BadRequestObjectResult with status code 400.
    /// </summary>
    public sealed class CustomizedResponseProvider : DefaultErrorResponseProvider
    {
        /// <summary>
        /// Creates a customized response for the specified error context.
        /// Returns a BadRequestObjectResult with status code 400.
        /// </summary>
        /// <param name="context">The error context.</param>
        /// <returns>The customized response.</returns>
        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            BadRequestObjectResult badRequestObjectResult = new(null)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            return badRequestObjectResult;
        }
    }
}
