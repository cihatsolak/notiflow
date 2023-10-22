namespace Notiflow.Backoffice.Application.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IStringLocalizer<ValidationErrorCodes> _validationErrorLocalizer;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        IStringLocalizer<ValidationErrorCodes> validationErrorLocalizer,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _jsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        _validationErrorLocalizer = validationErrorLocalizer;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException exception)
        {
            await ValidationProblemAsync(httpContext, exception);
        }
        catch
        {
            await InternalServerProblemAsync(httpContext);
        }
    }

    private Task ValidationProblemAsync(HttpContext httpContext, ValidationException validationException)
    {
        Result<EmptyResponse> validationErrorResponse = new()
        {
            Message = validationException.Errors.FirstOrDefault()?.ErrorMessage,
            Errors = validationException.Errors.Select(p => p.ErrorMessage)
        };

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return httpContext.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse, _jsonSerializerOptions));
    }

    private Task InternalServerProblemAsync(HttpContext httpContext)
    {
        var serverErrorResponse = Result<EmptyResponse>.Failure(ValidationErrorCodes.GENERAL_ERROR);
        serverErrorResponse.Message = "";
        //serverErrorResponse.Message = _responseLocalizer[$"{ResponseCodes.Error.GENERAL}"];

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return httpContext.Response.WriteAsync(JsonSerializer.Serialize(serverErrorResponse, _jsonSerializerOptions));
    }
}
