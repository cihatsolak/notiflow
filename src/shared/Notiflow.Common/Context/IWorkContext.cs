namespace Notiflow.Common.Context;

public interface IWorkContext
{
    int TenantId { get; }
    Guid TenantToken { get; }
    string DefaultConnectionString { get; }
    string ConnectionString<TDbContext>() where TDbContext : class, new();
}
