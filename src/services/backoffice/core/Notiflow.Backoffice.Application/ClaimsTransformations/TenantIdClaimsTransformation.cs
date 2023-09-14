namespace Notiflow.Backoffice.Application.ClaimsTransformations;

public sealed class TenantIdClaimsTransformation : IClaimsTransformation
{
    private readonly IRedisService _redisService;
    private readonly ILogger<TenantIdClaimsTransformation> _logger;

    public TenantIdClaimsTransformation(IRedisService redisService, ILogger<TenantIdClaimsTransformation> logger)
    {
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal is null || !principal.Identity.IsAuthenticated)
        {
            _logger.LogWarning("A tenant ID cannot be given to a non-claimed or unauthorized user.");

            return principal;
        }

        int tenantId = await _redisService.HashGetAsync<int>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_ID);
        if (0 > tenantId)
        {
            throw new TenantException("No tenant identification information was found in the cache.");
        }

        Claim tenantIdClaim = new(ClaimTypes.PrimaryGroupSid, $"{tenantId}", ClaimValueTypes.Integer32);

        ClaimsIdentity claimsIdentity = principal.Identity as ClaimsIdentity;
        claimsIdentity.AddClaim(tenantIdClaim);

        return principal;
    }
}
