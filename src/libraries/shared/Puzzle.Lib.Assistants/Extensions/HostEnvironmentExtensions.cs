namespace Puzzle.Lib.Assistants.Extensions
{
    /// <summary>
    /// Contains extension methods for the <see cref="IHostEnvironment"/> interface related to localhost environment.
    /// </summary>
    public static class HostEnvironmentExtensions
    {
        /// <summary>
        /// Determines whether the current environment is localhost.
        /// </summary>
        /// <param name="hostEnvironment">The current <see cref="IHostEnvironment"/> instance.</param>
        /// <returns><c>true</c> if the current environment is localhost; otherwise, <c>false</c>.</returns>
        public static bool IsLocalhost(this IHostEnvironment hostEnvironment) => hostEnvironment.IsEnvironment("Localhost");
    }
}
