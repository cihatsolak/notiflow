namespace Notiflow.Backoffice.Application.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IStringLocalizer<ValidationErrorLanguage> _validationErrorLocalizer;
    private readonly IStringLocalizer<ResponseLanguage> _responseLocalizer;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        IStringLocalizer<ValidationErrorLanguage> validationErrorLocalizer,
        IStringLocalizer<ResponseLanguage> responseLocalizer,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _jsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        _validationErrorLocalizer = validationErrorLocalizer;
        _responseLocalizer = responseLocalizer;
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
            _logger.LogError(exception, exception.Message);
            await ValidationProblemAsync(httpContext, exception);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await InternalServerProblemAsync(httpContext);
        }
    }

    private Task ValidationProblemAsync(HttpContext httpContext, Exception exception)
    {
        ApiResponse<EmptyResponse> validationErrorResponse = null;

        ValidationException validationException = exception as ValidationException;
        if (!validationException.Errors.Any())
        {
            validationErrorResponse = ApiResponse<EmptyResponse>.Failure(FluentValidationErrorCodes.GENERAL_ERROR);
            validationErrorResponse.Message = _validationErrorLocalizer[FluentValidationErrorCodes.GENERAL_ERROR.ToString()];
        }
        else
        {
            string errorCodeText = validationException.Errors.First().ErrorMessage;
            validationErrorResponse = new ApiResponse<EmptyResponse>()
            {
                HttpStatusCode = int.Parse(errorCodeText),
                Message = _validationErrorLocalizer[errorCodeText].Value,
                Errors = validationException.Errors.Select(p => p.ErrorMessage)
            };
        }

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return httpContext.Response.WriteAsync(JsonSerializer.Serialize(validationErrorResponse, _jsonSerializerOptions));
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
