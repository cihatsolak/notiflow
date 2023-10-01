namespace Notiflow.Backoffice.Application.Interfaces.UnitOfWorks;

public interface INotiflowUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    ICustomerReadRepository CustomerRead { get; }
    ICustomerWriteRepository CustomerWrite { get; }
    IDeviceReadRepository DeviceRead { get; }
    IDeviceWriteRepository DeviceWrite { get; }
    ITextMessageHistoryReadRepository TextMessageHistoryRead { get; }
    ITextMessageHistoryWriteRepository TextMessageHistoryWrite { get; }
    INotificationHistoryReadRepository NotificationHistoryRead { get; }
    INotificationHistoryWriteRepository NotificationHistoryWrite { get; }
    IEmailHistoryReadRepository EmailHistoryRead { get; }
    IEmailHistoryWriteRepository EmailHistoryWrite { get; }

    TRepository Repository<TRepository>();
}