namespace Notiflow.IdentityServer.Service.Tenants;

internal class TenantManager : ITenantService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<TenantManager> _logger;

    public TenantManager(
        IHttpContextAccessor httpContextAccessor, 
        ILogger<TenantManager> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public Guid Token => GetTenantTokenFromRequest();

    private Guid GetTenantTokenFromRequest()
    {
        bool isExists = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("x-tenant-token", out StringValues tenantToken);
        if (!isExists)
        {
            _logger.LogWarning("");
            throw new Exception("todo");
        }

        return Guid.Parse(tenantToken.First());
    }
}
