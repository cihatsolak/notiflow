namespace Notiflow.Lib.HealthCheck.Infrastructure.Settings
{
    internal interface IHealthSetting
    {
        string Name { get; init; }
        string Uri { get; init; }
        string Payload { get; init; }
        string RestorePayload { get; init; }
        int EvaluationTimeInSeconds { get; init; }
        int ApiMaxActiveRequests { get; init; }
        int MaximumHistoryEntriesPerEndpoint { get; init; }
        string ResponsePath { get; init; }
        string ViewUrl { get; init; }
        string CustomCssPath { get; init; }
    }

    internal sealed record HealthSetting : IHealthSetting
    {
        public string Name { get; init; }
        public string Uri { get; init; }
        public string Payload { get; init; }
        public string RestorePayload { get; init; }
        public int EvaluationTimeInSeconds { get; init; }
        public int ApiMaxActiveRequests { get; init; }
        public int MaximumHistoryEntriesPerEndpoint { get; init; }
        public string ResponsePath { get; init; }
        public string ViewUrl { get; init; }
        public string CustomCssPath { get; init; }
    }
}
