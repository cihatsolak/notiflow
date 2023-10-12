
namespace Notiflow.Backoffice.Application.Features.Queries.Languages.SupportedCultures;

public sealed class SupportedCulturesQueryHandler : IRequestHandler<SupportedCulturesQuery, ApiResponse<IEnumerable<SupportedCulturesQueryResult>>>
{
    private readonly RequestLocalizationOptions _localizationOptions;

    public SupportedCulturesQueryHandler(
        IOptions<RequestLocalizationOptions> localizationOptions)
    {
        _localizationOptions = localizationOptions.Value;
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
            return Task.FromResult(ApiResponse<IEnumerable<SupportedCulturesQueryResult>>.Fail(ResponseCodes.Error.SUPPORTED_LANGUAGES_NOT_FOUND));
        }

        return Task.FromResult(ApiResponse<IEnumerable<SupportedCulturesQueryResult>>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, supportedCultures));
    }
}
