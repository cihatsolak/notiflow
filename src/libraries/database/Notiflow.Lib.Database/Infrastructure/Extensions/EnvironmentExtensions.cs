namespace Notiflow.Lib.Database.Infrastructure.Extensions
{
    internal static class EnvironmentExtensions
    {
        internal static bool IsLiveEnvironment(this IWebHostEnvironment webHostEnvironment) => webHostEnvironment.IsProduction() || webHostEnvironment.IsStaging();
        internal static bool IsDevEnvironment(this IWebHostEnvironment webHostEnvironment) => !IsLiveEnvironment(webHostEnvironment);
    }
}
