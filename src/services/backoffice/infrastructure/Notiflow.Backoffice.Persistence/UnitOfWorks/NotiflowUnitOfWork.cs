namespace Notiflow.Backoffice.Persistence.UnitOfWorks;

internal sealed class NotiflowUnitOfWork : INotiflowUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;
    private readonly NotiflowDbContext _context;

    public NotiflowUnitOfWork(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _context = serviceProvider.GetRequiredService<NotiflowDbContext>();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
       => _context.Database.BeginTransactionAsync(cancellationToken);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);

    public async Task UndoChangesAsync(CancellationToken cancellationToken)
    {
        foreach (var entry in _context.ChangeTracker.Entries()
            .Where(e => e.State != EntityState.Unchanged))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;

                case EntityState.Modified:
                case EntityState.Deleted:
                    await entry.ReloadAsync(cancellationToken);
                    break;
            }
        }
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

    public TRepository Repository<TRepository>() => _serviceProvider.GetRequiredService<TRepository>();
}
