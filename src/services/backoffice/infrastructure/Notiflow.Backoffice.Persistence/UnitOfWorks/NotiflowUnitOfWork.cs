using Puzzle.Lib.Entities.Entities.SoftDelete;

namespace Notiflow.Backoffice.Persistence.UnitOfWorks;

internal sealed class NotiflowUnitOfWork : BaseUnitOfWork, INotiflowUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;

    public NotiflowUnitOfWork(
        NotiflowDbContext context,
        IServiceProvider serviceProvider) : base(context)
    {
        _serviceProvider = serviceProvider;
    }

    public ICustomerReadRepository CustomerRead => _serviceProvider.GetRequiredService<ICustomerReadRepository>();

    public ICustomerWriteRepository CustomerWrite => _serviceProvider.GetRequiredService<ICustomerWriteRepository>();

    public ITenantReadRepository TenantRead => _serviceProvider.GetRequiredService<ITenantReadRepository>();

    public ITenantWriteRepository TenantWrite => _serviceProvider.GetRequiredService<ITenantWriteRepository>();

    public IDeviceReadRepository DeviceRead => _serviceProvider.GetRequiredService<IDeviceReadRepository>();

    public IDeviceWriteRepository DeviceWrite => _serviceProvider.GetRequiredService<IDeviceWriteRepository>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var baseHistoricalEntities = _context.ChangeTracker.Entries<BaseHistoricalEntity>()
            .Where(p => p.State == EntityState.Added || p.State == EntityState.Modified);

        foreach (var baseHistoricalEntity in baseHistoricalEntities)
        {
            baseHistoricalEntity.Property(p => p.CreatedDate).IsModified = false;
            baseHistoricalEntity.Property(p => p.UpdatedDate).IsModified = false;
        }

        var baseSoftDeleteEntities = _context.ChangeTracker.Entries<BaseSoftDeleteEntity>();

        foreach (var baseSoftDeleteEntity in baseSoftDeleteEntities)
        {
            baseSoftDeleteEntity.State = EntityState.Modified;
            baseSoftDeleteEntity.Property(p => p.IsDeleted).CurrentValue = true;
        }

        return _context.SaveChangesAsync(cancellationToken);
    }
}
