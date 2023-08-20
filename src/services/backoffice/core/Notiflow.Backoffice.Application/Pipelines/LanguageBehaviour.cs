namespace Notiflow.Backoffice.Application.Pipelines;

public sealed class LanguageBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IStringLocalizer<MultiLanguage> _localizer;
    private readonly ILogger<LanguageBehaviour<TRequest, TResponse>> _logger;

    public LanguageBehaviour(
        IStringLocalizer<MultiLanguage> localizer,
        ILogger<LanguageBehaviour<TRequest, TResponse>> logger)
    {
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next();

        Type source = response.GetType();

        int code = (int)source.GetProperty("Code").GetValue(response);
        if (Math.Sign(code) != 1)
        {
            _logger.LogWarning("There is no multilingual message for the response code. Code to be served: {@code}", code);

            source.GetProperty("Message").SetValue(source, string.Empty, null);
        }
        else
        {
            source.GetProperty("Message").SetValue(response, _localizer[$"{code}"].Value, null);
        }

        return response;
    }
}
