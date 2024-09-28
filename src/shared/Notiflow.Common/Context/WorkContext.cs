namespace Notiflow.Common.Context;

public class WorkContext(IHttpContextAccessor context, IConfiguration configuration) : IWorkContext
{
    private const string UNAUTHORIZED_MESSAGE = "Unauthorized transaction detected.";
    private const string TENANT_TOKEN_HEADER = "X-Tenant-Token";
    private const string CONNECTION_STRING_HEADER = "X-Connection-String";

    public int TenantId => GetTenantId();
    public Guid TenantToken => GetTenantToken();

    public string DefaultConnectionString
    {
        get
        {
            if (context?.HttpContext?.Request.Headers.ContainsKey(CONNECTION_STRING_HEADER) ?? false)
                return context.HttpContext.Request.Headers[CONNECTION_STRING_HEADER].ToString();

            return string.Empty;
        }
    }

    public string ConnectionString<TDbContext>() where TDbContext : class, new()
    {
        return configuration[$"ConnectionStrings:{nameof(TDbContext)}"];
    }

    private Guid GetTenantToken()
    {
        if (context?.HttpContext?.Request?.Headers is null)
            throw new TenantException(UNAUTHORIZED_MESSAGE);

        if (!context.HttpContext.Request.Headers.TryGetValue(TENANT_TOKEN_HEADER, out var tenantTokenHeader))
            throw new TenantException(UNAUTHORIZED_MESSAGE);

        if (!Guid.TryParse(tenantTokenHeader.FirstOrDefault(), out var tenantToken))
            throw new TenantException(UNAUTHORIZED_MESSAGE);

        return tenantToken;
    }

    private int GetTenantId()
    {
        Claim tenantIdClaim = context.HttpContext?.User?.Claims?.SingleOrDefault(claim => claim.Type == ClaimTypes.PrimaryGroupSid);

        if (tenantIdClaim is null || !int.TryParse(tenantIdClaim.Value, out int tenantId))
        {
            throw new TenantException(UNAUTHORIZED_MESSAGE);
        }

        return tenantId;
    }    
}