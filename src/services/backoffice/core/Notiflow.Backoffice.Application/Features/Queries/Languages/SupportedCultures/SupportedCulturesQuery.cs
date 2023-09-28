namespace Notiflow.Backoffice.Application.Features.Queries.Languages.SupportedCultures;

public sealed record SupportedCulturesQuery : IRequest<ApiResponse<IEnumerable<SupportedCulturesQueryResult>>>;
