namespace Notiflow.Backoffice.Application.Features.Queries.Languages.SupportedCultures;

public sealed record SupportedCulturesQueryResponse
{
    public required string Name { get; init; }
    public required string DisplayName { get; init; }
}
