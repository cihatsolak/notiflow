namespace Notiflow.Lib.Auth.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
