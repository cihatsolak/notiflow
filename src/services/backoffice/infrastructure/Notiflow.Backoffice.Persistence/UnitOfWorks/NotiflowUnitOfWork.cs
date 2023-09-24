﻿namespace Notiflow.Backoffice.Persistence.UnitOfWorks;

internal sealed class NotiflowUnitOfWork : BaseUnitOfWork, INotiflowUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;

    public NotiflowUnitOfWork(IServiceProvider serviceProvider) : base(serviceProvider.GetRequiredService<NotiflowDbContext>())
    {
        _serviceProvider = serviceProvider;
    }

    public ICustomerReadRepository CustomerRead => _serviceProvider.GetRequiredService<ICustomerReadRepository>();

    public ICustomerWriteRepository CustomerWrite => _serviceProvider.GetRequiredService<ICustomerWriteRepository>();

    public IDeviceReadRepository DeviceRead => _serviceProvider.GetRequiredService<IDeviceReadRepository>();

    public IDeviceWriteRepository DeviceWrite => _serviceProvider.GetRequiredService<IDeviceWriteRepository>();

    public ITextMessageHistoryReadRepository TextMessageHistoryRead => _serviceProvider.GetRequiredService<ITextMessageHistoryReadRepository>();

    public ITextMessageHistoryWriteRepository TextMessageHistoryWrite => _serviceProvider.GetRequiredService<ITextMessageHistoryWriteRepository>();

    public INotificationHistoryReadRepository NotificationHistoryRead => _serviceProvider.GetRequiredService<INotificationHistoryReadRepository>();

    public INotificationHistoryWriteRepository NotificationHistoryWrite => _serviceProvider.GetRequiredService<INotificationHistoryWriteRepository>();

    public IEmailHistoryReadRepository EmailHistoryRead => _serviceProvider.GetRequiredService<IEmailHistoryReadRepository>();

    public IEmailHistoryWriteRepository EmailHistoryWrite => _serviceProvider.GetRequiredService<IEmailHistoryWriteRepository>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var baseHistoricalEntities = _context.ChangeTracker.Entries<IBaseHistoricalEntity>();

        foreach (var baseHistoricalEntity in baseHistoricalEntities)
        {
            switch (baseHistoricalEntity.State)
            {
                case EntityState.Added:
                    baseHistoricalEntity.Property(p => p.UpdatedDate).IsModified = false;
                    baseHistoricalEntity.Entity.CreatedDate = DateTime.Now;
                    break;

                case EntityState.Modified:
                    baseHistoricalEntity.Property(p => p.CreatedDate).IsModified = false;
                    baseHistoricalEntity.Entity.UpdatedDate = DateTime.Now;
                    break;
            }
        }

        var baseSoftDeleteEntities = _context.ChangeTracker.Entries<IBaseSoftDeleteEntity>();

        foreach (var baseSoftDeleteEntity in baseSoftDeleteEntities)
        {
            baseSoftDeleteEntity.State = EntityState.Modified;
            baseSoftDeleteEntity.Property(p => p.IsDeleted).CurrentValue = true;
        }

        return _context.SaveChangesAsync(cancellationToken);
    }
}
