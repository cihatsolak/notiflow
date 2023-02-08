global using Microsoft.AspNetCore.Builder;

namespace Notiflow.Lib.Performance.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Add response compression
        /// </summary>
        /// <param name="app">type of web application</param>
        /// <returns>type of web application</returns>
        /// <see cref="https://learn.microsoft.com/en-us/aspnet/core/performance/response-compression?view=aspnetcore-7.0"/>
        public static IApplicationBuilder UseResponseCompress(this IApplicationBuilder app)
        {
            return app.UseResponseCompression();
        }
    }
}
