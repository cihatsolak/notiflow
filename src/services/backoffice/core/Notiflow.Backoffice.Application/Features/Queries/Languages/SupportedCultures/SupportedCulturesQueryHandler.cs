
namespace Notiflow.Backoffice.Application.Features.Queries.Languages.SupportedCultures;

public sealed class SupportedCulturesQueryHandler : IRequestHandler<SupportedCulturesQuery, Response<IEnumerable<SupportedCulturesQueryResponse>>>
{
    private readonly RequestLocalizationOptions _localizationOptions;
    private readonly ILogger<SupportedCulturesQueryHandler> _logger;

    public SupportedCulturesQueryHandler(
        IOptions<RequestLocalizationOptions> localizationOptions,
        ILogger<SupportedCulturesQueryHandler> logger)
    {
        _localizationOptions = localizationOptions.Value;
        _logger = logger;
    }

    public Task<Response<IEnumerable<SupportedCulturesQueryResponse>>> Handle(SupportedCulturesQuery request, CancellationToken cancellationToken)
    {
        var supportedCultures = _localizationOptions.SupportedCultures
                .Select(culture => new SupportedCulturesQueryResponse
                {
                    Name = culture.Name,
                    DisplayName = culture.DisplayName
                });

        if (supportedCultures.IsNullOrNotAny())
        {
            _logger.LogInformation("No supported language found.");
            return Task.FromResult(Response<IEnumerable<SupportedCulturesQueryResponse>>.Fail(ResponseCodes.Error.SUPPORTED_LANGUAGES_NOT_FOUND));
        }

        return Task.FromResult(Response<IEnumerable<SupportedCulturesQueryResponse>>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, supportedCultures));
    }
}
