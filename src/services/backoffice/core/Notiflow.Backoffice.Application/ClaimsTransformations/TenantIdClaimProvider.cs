namespace Notiflow.Backoffice.Application.ClaimsTransformations;

public sealed class TenantIdClaimProvider : IClaimsTransformation
{
    private readonly IRedisService _redisService;
    private readonly ILogger<TenantIdClaimProvider> _logger;

    public TenantIdClaimProvider(IRedisService redisService, ILogger<TenantIdClaimProvider> logger)
    {
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal is null || principal.Identity.IsAuthenticated)
        {
            _logger.LogWarning("A tenant ID cannot be given to a non-claimed or unauthorized user.");

            return principal;
        }

        string tenantId = await _redisService.HashGetAsync<string>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_ID);

        Claim tenantIdClaim = new(ClaimTypes.PrimaryGroupSid, tenantId, ClaimValueTypes.Integer32);

        ClaimsIdentity claimsIdentity = principal.Identity as ClaimsIdentity;
        claimsIdentity.AddClaim(tenantIdClaim);

        return principal;
    }
}
