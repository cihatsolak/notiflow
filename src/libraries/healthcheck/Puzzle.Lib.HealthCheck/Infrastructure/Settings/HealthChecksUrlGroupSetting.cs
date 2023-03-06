namespace Puzzle.Lib.HealthCheck.Infrastructure.Settings
{
    internal interface IHealthChecksUrlGroupSetting
    {
        List<UrlGroupInformation> UrlGroups { get; init; }
    }

    internal sealed record HealthChecksUrlGroupSetting : IHealthChecksUrlGroupSetting
    {
        public List<UrlGroupInformation> UrlGroups { get; init; }
    }

    internal sealed record UrlGroupInformation
    {
        public Uri ServiceUri { get; init; }
        public string Name { get; init; }
        public string[] Tags { get; init; }
    }
}
