using Notiflow.Common.Localize;

namespace Notiflow.Backoffice.Application.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IStringLocalizer<ValidationErrorCodes> _responseLocalizer;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        IStringLocalizer<ValidationErrorCodes> responseLocalizer,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _jsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        _responseLocalizer = responseLocalizer;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception) when (exception is not ValidationException)
        {
            _logger.LogError(exception, exception.Message);
            await InternalServerProblemAsync(httpContext);
        }
    }

    private Task InternalServerProblemAsync(HttpContext httpContext)
    {
        var serverErrorResponse = ApiResponse<EmptyResponse>.Failure(ResponseCodes.Error.GENERAL);
        serverErrorResponse.Message = _responseLocalizer[$"{ResponseCodes.Error.GENERAL}"];

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return httpContext.Response.WriteAsync(JsonSerializer.Serialize(serverErrorResponse, _jsonSerializerOptions));
    }
}
