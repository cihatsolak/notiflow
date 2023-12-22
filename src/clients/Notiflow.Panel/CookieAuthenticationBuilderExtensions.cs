namespace Notiflow.Panel;

internal static class CookieAuthenticationBuilderExtensions
{
    internal static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configure =>
                   {
                       configure.LoginPath = new("/Authentication/SignIn");
                       configure.LogoutPath = new("/Authentication/SignOut");
                       configure.AccessDeniedPath = new PathString("/Authentication/Forbidden");
                       configure.ExpireTimeSpan = TimeSpan.FromHours(2);
                       configure.SlidingExpiration = false;
                       configure.Cookie = new()
                       {
                           Name = Assembly.GetEntryAssembly().GetName().Name.ToLower(CultureInfo.InvariantCulture),
                           HttpOnly = true,
                           SameSite = SameSiteMode.Strict,
                           SecurePolicy = CookieSecurePolicy.Always
                       };
                   });

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.Secure = CookieSecurePolicy.Always;
            options.HttpOnly = HttpOnlyPolicy.Always;
            options.MinimumSameSitePolicy = SameSiteMode.Strict;
        });

        return services;
    }
}
