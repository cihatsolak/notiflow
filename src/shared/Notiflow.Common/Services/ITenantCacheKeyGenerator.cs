namespace Notiflow.Common.Services;

public interface ITenantCacheKeyGenerator
{
    string GenerateCacheKey(string key);
    string GenerateCacheKey(string key, Guid tenantToken);
    string GenerateCacheKey(string key, int tenantId);
}

internal sealed class TenantCacheKeyGenerator : ITenantCacheKeyGenerator
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantCacheKeyGenerator(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GenerateCacheKey(string key)
    {
        if (_httpContextAccessor.HttpContext is null)
            throw new Exception(); //TODO

        bool isExists = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("x-tenant-token", out StringValues tenantToken);
        if (!isExists)
        {
            throw new Exception(); //TODOs
        }

        return string.Concat(key, ".", tenantToken.Single());
    }

    public string GenerateCacheKey(string key, Guid tenantToken) => string.Concat(key, ".", tenantToken);

    public string GenerateCacheKey(string key, int tenantId) => string.Concat(key, ".", tenantId);
}

