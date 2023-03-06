namespace Puzzle.Lib.Version.Infrastructure.Constants
{
    public static class RouteSchema
    {
        /// <summary>
        /// Unversioned schema
        /// </summary>
        public const string WithAction = "api/[controller]/[action]";

        /// <summary>
        /// Schema without action
        /// </summary>
        public const string WithoutAction = "api/v1/[controller]";

        /// <summary>
        /// Schema with conventional version
        /// </summary>
        public const string Standart = "api/v1/[controller]/[action]";

        /// <summary>
        /// Versionated schema
        /// </summary>
        public const string Versioned = "api/v{version:apiVersion}/[controller]/[action]";
    }
}
