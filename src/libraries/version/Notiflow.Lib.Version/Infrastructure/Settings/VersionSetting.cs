namespace Notiflow.Lib.Version.Infrastructure.Settings
{
    public interface IApiVersionSetting
    {
        string HeaderName { get; init; }
        int MajorVersion { get; init; }
        int MinorVersion { get; init; }
    }

    internal sealed record ApiVersionSetting : IApiVersionSetting
    {
        public string HeaderName { get; init; }
        public int MajorVersion { get; init; }
        public int MinorVersion { get; init; }
    }
}
