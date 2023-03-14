namespace Puzzle.Lib.Version.Infrastructure.Constants
{
    /// <summary>
    /// Contains constants for various route schemas.
    /// </summary>
    public static class RouteSchema
    {
        /// <summary>
        /// The route schema for actions with controllers.
        /// </summary>
        public const string WithAction = "api/[controller]/[action]";

        /// <summary>
        /// The route schema for controllers without actions.
        /// </summary>
        public const string WithoutAction = "api/v1/[controller]";

        /// <summary>
        /// The standard route schema for controllers and actions.
        /// </summary>
        public const string Standart = "api/v1/[controller]/[action]";

        /// <summary>
        /// The route schema for versioned controllers and actions.
        /// </summary>
        public const string Versioned = "api/v{version:apiVersion}/[controller]/[action]";
    }
}
