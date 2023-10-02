
namespace Notiflow.Backoffice.Application.Features.Queries.Languages.SupportedCultures;

public sealed class SupportedCulturesQueryHandler : IRequestHandler<SupportedCulturesQuery, ApiResponse<IEnumerable<SupportedCulturesQueryResult>>>
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

    public Task<ApiResponse<IEnumerable<SupportedCulturesQueryResult>>> Handle(SupportedCulturesQuery request, CancellationToken cancellationToken)
    {
        var supportedCultures = _localizationOptions.SupportedCultures
                .Select(culture => new SupportedCulturesQueryResult
                {
                    Name = culture.Name,
                    DisplayName = culture.DisplayName
                });

        if (supportedCultures.IsNullOrNotAny())
        {
            _logger.LogInformation("No supported language found.");
            return Task.FromResult(ApiResponse<IEnumerable<SupportedCulturesQueryResult>>.Fail(ResponseCodes.Error.SUPPORTED_LANGUAGES_NOT_FOUND));
        }

        return Task.FromResult(ApiResponse<IEnumerable<SupportedCulturesQueryResult>>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, supportedCultures));
    }
}
