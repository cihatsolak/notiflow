namespace Notiflow.Backoffice.Application.ClaimsTransformations;

public sealed class TenantIdClaimsTransformation : IClaimsTransformation
{
    private readonly IRedisService _redisService;

    public TenantIdClaimsTransformation(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal is null || !principal.Identity.IsAuthenticated)
        {
            throw new TenantException("A tenant ID cannot be given to a non-claimed or unauthorized user.");
        }

        int tenantId = await _redisService.HashGetAsync<int>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_ID);
        if (0 >= tenantId)
        {
            throw new TenantException("No tenant identification information was found in the cache.");
        }

        Claim tenantIdClaim = new(ClaimTypes.PrimaryGroupSid, $"{tenantId}", ClaimValueTypes.Integer32);

        ClaimsIdentity claimsIdentity = principal.Identity as ClaimsIdentity;
        claimsIdentity.AddClaim(tenantIdClaim);

        return principal;
    }
}
