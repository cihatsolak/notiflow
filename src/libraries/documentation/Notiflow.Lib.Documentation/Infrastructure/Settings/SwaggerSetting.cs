namespace Notiflow.Lib.Documentation.Infrastructure.Settings
{
    public interface ISwaggerSetting
    {
        string DefinitionName { get; init; }
        string Title { get; init; }
        string Description { get; init; }
        string Version { get; init; }
        string TermsOfService { get; init; }
        string ContactName { get; init; }
        string ContactUrl { get; init; }
        string ContactEmail { get; init; }
        string LicenseName { get; init; }
        string LicenseUrl { get; init; }
        string LogoUrl { get; init; }
        bool IsClosedSchema { get; init; }
        bool IsBasicSecurityScheme { get; init; }
        bool IsJwtSecurityScheme { get; init; }
    }

    public sealed record SwaggerSetting : ISwaggerSetting
    {
        public string DefinitionName { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Version { get; init; }
        public string TermsOfService { get; init; }
        public string ContactName { get; init; }
        public string ContactUrl { get; init; }
        public string ContactEmail { get; init; }
        public string LicenseName { get; init; }
        public string LicenseUrl { get; init; }
        public string LogoUrl { get; init; }
        public bool IsClosedSchema { get; init; }
        public bool IsBasicSecurityScheme { get; init; }
        public bool IsJwtSecurityScheme { get; init; }
    }
}