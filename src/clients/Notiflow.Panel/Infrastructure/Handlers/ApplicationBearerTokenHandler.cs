namespace Notiflow.Panel.Infrastructure.Handlers;

internal sealed class ApplicationBearerTokenHandler(
    IHttpContextAccessor httpContextAccessor, 
    IAuthService authService) : DelegatingHandler
{
    private const string BEARER_SCHEMA = "Bearer";
    private static readonly int[] AUTH_PROBLEM_CODES = [StatusCodes.Status401Unauthorized, StatusCodes.Status403Forbidden];

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string accessToken = await httpContextAccessor.HttpContext.GetTokenAsync(AuthParameterNames.AccessToken);
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.Headers.Authorization = new(BEARER_SCHEMA, accessToken);
        }

        HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken);
        if (Array.Exists(AUTH_PROBLEM_CODES, statusCode => statusCode == httpResponseMessage.StatusCode.GetHashCode()))
        {
            var token = await authService.GetAccessTokenByRefreshTokenAsync(cancellationToken);

            request.Headers.Authorization = new(BEARER_SCHEMA, token.AccessToken);
            httpResponseMessage = await base.SendAsync(request, cancellationToken);
        }

        if (Array.Exists(AUTH_PROBLEM_CODES, statusCode => statusCode == httpResponseMessage.StatusCode.GetHashCode()))
        {
            throw new UnauthorizedAccessException("unauthorized access.");           
        }
        
        return httpResponseMessage;
    }
}
