namespace Puzzle.Lib.Logging.Middlewares
{
    /// <summary>
    /// Application builder extensions
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use seri log main push property logging
        /// </summary>
        /// <param name="app">type of application builder</param>
        /// <returns>type of application builder</returns>
        public static IApplicationBuilder UseHttpRequestPropertyLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HttpRequestPropertyMiddleware>();
        }

        /// <summary>
        /// Use correlation id logging
        /// </summary>
        /// <param name="app">type of application builder</param>
        /// <returns>type of application builder</returns>
        public static IApplicationBuilder UseCorrelationIdLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CorrelationIdMiddleware>();
        }

        public static IApplicationBuilder UseCustomSeriLogging(this IApplicationBuilder app) 
        {
            return app.UseSerilogRequestLogging().UseHttpRequestPropertyLogging().UseCorrelationIdLogging();
        }

        public static IApplicationBuilder UseCustomHttpLogging(this IApplicationBuilder app)
        {
            return app.UseHttpLogging();
        }
    }
}
