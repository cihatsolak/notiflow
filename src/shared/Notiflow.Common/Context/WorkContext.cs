namespace Notiflow.Common.Context;

public interface IWorkContext
{
    int TenantId { get; }
    string ConnectionString { get; }
}

public class WorkContext(IHttpContextAccessor context) : IWorkContext
{
    public int TenantId
    {
        get
        {
            if (context.HttpContext.Request.Headers.TryGetValue("x-tenant-token", out StringValues asd))
            {

            }

            if (context?.HttpContext?.Request.Headers.ContainsKey() ?? false)
                return Convert.ToInt32(context.HttpContext.Request.Headers["X-TenantId"]);

            return 0;
        }
    }

    public string ConnectionString
    {
        get
        {
            if (context?.HttpContext?.Request.Headers.ContainsKey("X-Connection-String") ?? false)
                return context.HttpContext.Request.Headers["X-Connection-String"].ToString();

            return string.Empty;
        }
    }
}
