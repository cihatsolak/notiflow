namespace Notiflow.Backoffice.Application.Filters;

public class TenantTokenAuthenticationFilter : IAuthorizationFilter
{
    private readonly IRedisService _redisService;

    public TenantTokenAuthenticationFilter(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool isValidHeader = context.HttpContext.Request.Headers.TryGetValue("X-Tenant-Token", out StringValues headerTenantToken);
        if (!isValidHeader)
        {
            context.Result = new UnauthorizedObjectResult(ErrorResponse);
            return;
        }

        bool isValidToken = Guid.TryParse(headerTenantToken.FirstOrDefault(), out Guid tenantToken);
        if (!isValidToken)
        {
            context.Result = new UnauthorizedObjectResult(ErrorResponse);
            return;
        }

        bool isExists = _redisService.SetExistsAsync(CacheKeys.TENANT_TOKENS, tenantToken).Result;
        if (!isExists)
        {
            context.Result = new UnauthorizedObjectResult(ErrorResponse);
        }
    }

    public static Response<EmptyResponse> ErrorResponse => new()
    {
        Code = 1,
        Message = "Missing X-Tenant-Token header."
    };
}
