namespace Puzzle.Lib.Documentation.Middlewares;

public sealed class SwaggerBasicAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SwaggerSecuritySetting _swaggerSecuritySetting;

    public SwaggerBasicAuthenticationMiddleware(
        RequestDelegate next,
        IOptions<SwaggerSecuritySetting> swaggerSecuritySetting)
    {
        _next = next;
        _swaggerSecuritySetting = swaggerSecuritySetting.Value;
    }

    public Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Path.StartsWithSegments("/swagger"))
        {
            return _next.Invoke(context);
        }

        string authenticationHader = context.Request.Headers[HeaderNames.Authorization];

        if (authenticationHader is not null && authenticationHader.StartsWith("Basic "))
        {
            string encodedUsernamePassword = authenticationHader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
            string decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

            string username = decodedUsernamePassword.Split(':', 2)[0];
            string password = decodedUsernamePassword.Split(':', 2)[1];

            if (IsAuthorized(username, password))
            {
                return _next.Invoke(context);
            }
        }

        context.Response.Headers.WWWAuthenticate = "Basic";
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        return _next.Invoke(context);
    }

    private bool IsAuthorized(string username, string password)
    {
        return username.Equals(_swaggerSecuritySetting.Username, StringComparison.InvariantCultureIgnoreCase) &&
            password.Equals(_swaggerSecuritySetting.Password);
    }
}