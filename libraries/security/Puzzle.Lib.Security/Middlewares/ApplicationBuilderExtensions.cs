namespace Puzzle.Lib.Security.Middlewares
{
    /// <summary>
    /// Provides extension methods for <see cref="IApplicationBuilder"/> to add request detection logging functionality.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds request detection logging functionality to the specified <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the request detection logging functionality to.</param>
        /// <returns>The modified <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseRequestDetection(this IApplicationBuilder app)
        {
            return app.UseDetection();
        }
    }
}
