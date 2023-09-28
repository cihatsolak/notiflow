namespace Notiflow.Backoffice.Infrastructure.Filters;

public sealed class TenantTokenAuthenticationFilter : IAsyncAuthorizationFilter
{
    private readonly IRedisService _redisService;
    private readonly ILogger<TenantTokenAuthenticationFilter> _logger;

    public TenantTokenAuthenticationFilter(
        IRedisService redisService,
        ILogger<TenantTokenAuthenticationFilter> logger)
    {
        _redisService = redisService;
        _logger = logger;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        bool isValidHeader = context.HttpContext.Request.Headers.TryGetValue("X-Tenant-Token", out StringValues headerTenantToken);
        if (!isValidHeader)
        {
            context.Result = new UnauthorizedObjectResult(MissingErrorResponse);
            return;
        }

        bool isValidToken = Guid.TryParse(headerTenantToken.FirstOrDefault(), out Guid tenantToken);
        if (!isValidToken)
        {
            context.Result = new UnauthorizedObjectResult(InvalidErrorResponse);
            return;
        }

        bool isExists = await _redisService.SetExistsAsync(CacheKeys.TENANT_TOKENS, tenantToken);
        if (!isExists)
        {
            _logger.LogInformation("A request was made with a valid tenant token: {@tenantToken}.", tenantToken);
            context.Result = new UnauthorizedObjectResult(InvalidErrorResponse);
        }
    }

    public static ApiResponse<EmptyResponse> InvalidErrorResponse => new()
    {
        Code = 1,
        Message = "Invalid X-Tenant-Token header."
    };

    public static ApiResponse<EmptyResponse> MissingErrorResponse => new()
    {
        Code = 1,
        Message = "Missing X-Tenant-Token header."
    };
}
