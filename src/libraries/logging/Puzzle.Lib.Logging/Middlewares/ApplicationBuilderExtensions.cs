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

        /// <summary>
        /// Use ip adress id logging
        /// </summary>
        /// <param name="app">type of application builder</param>
        /// <returns>type of application builder</returns>
        public static IApplicationBuilder UseIpAddressLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<IpAddressMiddleware>();
        }

        /// <summary>
        /// Use request detection logging
        /// </summary>
        /// <param name="app">type of application builder</param>
        /// <returns>type of application builder</returns>
        public static IApplicationBuilder UseRequestDetectionLogging(this IApplicationBuilder app)
        {
            app.UseDetection();
            return app.UseMiddleware<RequstDetectionMiddleware>();
        }

        public static IApplicationBuilder UseCustomSeriLogging(this IApplicationBuilder app) 
        {
            app.UseSerilogRequestLogging();

            app.UseHttpRequestPropertyLogging()
               .UseCorrelationIdLogging()
               .UseIpAddressLogging()
               .UseRequestDetectionLogging();

            return app;
        }
    }
}
