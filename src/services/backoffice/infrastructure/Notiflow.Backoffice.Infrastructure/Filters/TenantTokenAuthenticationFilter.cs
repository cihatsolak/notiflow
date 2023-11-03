namespace Notiflow.Backoffice.Infrastructure.Filters;

public sealed class TenantTokenAuthenticationFilter : IAsyncAuthorizationFilter
{
    private readonly IRedisService _redisService;
    private readonly ILocalizerService<ResultMessage> _localizer;
    private readonly ILogger<TenantTokenAuthenticationFilter> _logger;

    public TenantTokenAuthenticationFilter(
        IRedisService redisService,
        ILocalizerService<ResultMessage> localizer,
        ILogger<TenantTokenAuthenticationFilter> logger)
    {
        _redisService = redisService;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        bool isValidHeader = context.HttpContext.Request.Headers.TryGetValue("X-Tenant-Token", out StringValues headerTenantToken);
        if (!isValidHeader)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                message = _localizer[ResultMessage.TENANT_COULD_NOT_BE_IDENTIFIED]
            });
            return;
        }

        bool isValidToken = Guid.TryParse(headerTenantToken[0], out Guid tenantToken);
        if (!isValidToken)
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                message = _localizer[ResultMessage.TENANT_COULD_NOT_BE_IDENTIFIED]
            });
            return;
        }

        bool isExists = await _redisService.SetExistsAsync(CacheKeys.TENANT_TOKENS, tenantToken);
        if (!isExists)
        {
            _logger.LogInformation("A request was made with a valid tenant token: {tenantToken}.", tenantToken);
            context.Result = new UnauthorizedObjectResult(new
            {
                message = _localizer[ResultMessage.TENANT_COULD_NOT_BE_IDENTIFIED]
            });
        }
    }
}
