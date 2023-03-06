namespace Notiflow.Lib.Auth.Middlewares
{
    /// <summary>
    /// Extension methods to add authentication  and authorization capabilities to an application pipeline.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use middleware for authentication and authorization
        /// </summary>
        /// <param name="app">type of application builder interface</param>
        /// <returns>type of application builder interface</returns>
        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
