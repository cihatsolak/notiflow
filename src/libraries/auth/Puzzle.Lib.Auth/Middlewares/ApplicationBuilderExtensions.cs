namespace Puzzle.Lib.Auth.Middlewares
{
    /// <summary>
    /// Provides extension methods to add authentication and authorization middleware to the request pipeline.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds authentication and authorization middleware to the request pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance with middleware added.</returns>
        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            return app.UseAuthentication().UseAuthorization();
        }
    }
}
