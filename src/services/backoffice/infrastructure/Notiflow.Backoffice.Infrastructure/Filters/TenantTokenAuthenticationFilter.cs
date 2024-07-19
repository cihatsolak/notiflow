namespace Notiflow.Backoffice.Infrastructure.Filters;

public sealed class TenantTokenAuthenticationFilter(
    IRedisService redisService,
    ILocalizerService<ResultCodes> localizer,
    ILogger<TenantTokenAuthenticationFilter> logger) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        bool isValidHeader = context.HttpContext.Request.Headers.TryGetValue("X-Tenant-Token", out StringValues headerTenantToken);
        if (!isValidHeader)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                message = localizer[ResultCodes.TENANT_COULD_NOT_BE_IDENTIFIED]
            });
            return;
        }

        bool isValidToken = Guid.TryParse(headerTenantToken[0], out Guid tenantToken);
        if (!isValidToken)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                message = localizer[ResultCodes.TENANT_COULD_NOT_BE_IDENTIFIED]
            });
            return;
        }

        bool isExists = await redisService.SetExistsAsync(CacheKeys.TENANT_TOKENS, tenantToken);
        if (!isExists)
        {
            logger.LogInformation("A request was made with a valid tenant token: {tenantToken}.", tenantToken);
            context.Result = new UnauthorizedObjectResult(new
            {
                message = localizer[ResultCodes.TENANT_COULD_NOT_BE_IDENTIFIED]
            });
        }
    }
}
