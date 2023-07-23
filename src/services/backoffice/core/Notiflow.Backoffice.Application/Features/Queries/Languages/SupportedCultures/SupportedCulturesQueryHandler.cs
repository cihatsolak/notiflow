
namespace Notiflow.Backoffice.Application.Features.Queries.Languages.SupportedCultures;

public sealed class SupportedCulturesQueryHandler : IRequestHandler<SupportedCulturesQuery, Response<IEnumerable<SupportedCulturesQueryResponse>>>
{
    private readonly RequestLocalizationOptions _localizationOptions;

    public SupportedCulturesQueryHandler(IOptions<RequestLocalizationOptions> localizationOptions)
    {
        _localizationOptions = localizationOptions.Value;
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
            return Task.FromResult(Response<IEnumerable<SupportedCulturesQueryResponse>>.Fail(-1));
        }

        return Task.FromResult(Response<IEnumerable<SupportedCulturesQueryResponse>>.Success(-1, supportedCultures));
    }
}
