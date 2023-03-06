namespace Puzzle.Lib.Version.Providers
{
    public sealed class CustomizedResponseProvider : DefaultErrorResponseProvider
    {
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
