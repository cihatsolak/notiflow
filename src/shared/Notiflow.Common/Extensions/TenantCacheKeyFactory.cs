namespace Notiflow.Common.Extensions;

public static class TenantCacheKeyFactory
{
    private static IHttpContextAccessor _httpContextAccessor;

    public static void Configure(IApplicationBuilder applicationBuilder)
    {
        if (applicationBuilder is null)
        {
            throw new ArgumentNullException(nameof(applicationBuilder), "Middleware class configuration is needed to generate a cache key.");
        }

        _httpContextAccessor = applicationBuilder.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
    }

    public static string Generate(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        bool isExists = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("x-tenant-token", out StringValues tenantToken);
        if (!isExists)
        {
            throw new Exception(); //TODOs
        }

        return string.Concat(key, ".", tenantToken.Single().ToLowerInvariant());
    }

    public static string Generate(string key, Guid tenantToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        if (tenantToken == Guid.Empty)
        {
            throw new ArgumentException(nameof(tenantToken));
        }

        return string.Concat(key, ".", tenantToken.ToString().ToLowerInvariant());
    }

    public static string Generate(string key, int tenantId)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        if (Math.Sign(tenantId) != 1)
        {
            throw new ArgumentException(nameof(tenantId));
        }

        return string.Concat(key, ".", tenantId);
    }
}

