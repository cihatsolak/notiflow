namespace Notiflow.Backoffice.Persistence.Repositories.Devices;

public sealed class DeviceReadRepository : ReadRepository<Device>, IDeviceReadRepository
{
    public DeviceReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }

    public async Task<(int recordsTotal, List<Device> devices)> GetPageAsync(string sortKey, string searchKey, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var deviceTable = TableNoTrackingWithIdentityResolution.IgnoreQueryFilters();

        if (string.IsNullOrWhiteSpace(sortKey))
            deviceTable = deviceTable.OrderBy(sortKey);
        else
            deviceTable = deviceTable.OrderByDescending(device => device.CreatedDate);

        if (string.IsNullOrWhiteSpace(searchKey))
        {
            //deviceTable = deviceTable.Where(customer => string.Concat(customer.Name, customer.Surname, customer.PhoneNumber, customer.Email).IndexOf(searchKey) > -1);
        }

        int recordsTotal = await deviceTable.CountAsync(cancellationToken);

        var customers = await deviceTable
            .TagWith("Lists devices records by paging.")
            .Skip(pageIndex)
            .Take(pageSize)
            .Select(device => new Device
            {
                Id = device.Id,
                OSVersion = device.OSVersion,
                Code = device.Code,
                CloudMessagePlatform = device.CloudMessagePlatform,
                CreatedDate = device.CreatedDate,
                Customer = new Customer
                {
                    Name = device.Customer.Name,
                    Surname = device.Customer.Surname,
                }
            })
            .ToListAsync(cancellationToken);

        return (recordsTotal, customers);
    }

    public Task<Device> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
    {
        return TableNoTracking
               .TagWith("Queries the customer's device information.")
               .SingleOrDefaultAsync(device => device.CustomerId == customerId, cancellationToken);
    }

    public Task<Device> GetCloudMessagePlatformByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
    {
        return TableNoTracking
               .TagWith("Returns the customer's notification platform and the platform's token information.")
               .Where(device => device.CustomerId == customerId)
               .Select(device => new Device
               {
                   CloudMessagePlatform = device.CloudMessagePlatform,
                   Token = device.Token
               })
               .SingleOrDefaultAsync(cancellationToken);
    }

    public Task<List<Device>> GetCloudMessagePlatformByCustomerIdsAsync(List<int> customerIds, CancellationToken cancellationToken)
    {
        return TableNoTracking
               .TagWith("Returns the customer's notification platform and the platform's token information.")
               .Where(device => customerIds.Any(customerId => customerId == device.CustomerId))
               .Select(device => new Device
               {
                   CloudMessagePlatform = device.CloudMessagePlatform,
                   Token = device.Token
               })
              .ToListAsync(cancellationToken);
    }
}
