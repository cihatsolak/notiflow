namespace Puzzle.Lib.Assistants.Extensions
{
    public static class HostEnvironmentExtensions
    {
        public static bool IsLocalhost(this IHostEnvironment hostEnvironment) => hostEnvironment.IsEnvironment("Localhost");
    }
}
