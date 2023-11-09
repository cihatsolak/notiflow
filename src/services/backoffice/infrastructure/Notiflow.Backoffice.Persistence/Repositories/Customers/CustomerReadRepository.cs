namespace Notiflow.Backoffice.Persistence.Repositories.Customers;

public sealed class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }

    public async Task<(int recordsTotal, List<Customer> customers)> GetPageAsync(string sortKey, string searchKey, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var customerTable = TableNoTrackingWithIdentityResolution.IgnoreQueryFilters();

        if (string.IsNullOrWhiteSpace(sortKey))
            customerTable = customerTable.OrderBy(sortKey);
        else
            customerTable = customerTable.OrderByDescending(customer => customer.CreatedDate);

        if (string.IsNullOrWhiteSpace(searchKey))
        {
            customerTable = customerTable.Where(customer => string.Concat(customer.Name, customer.Surname, customer.PhoneNumber, customer.Email).IndexOf(searchKey) > -1);
        }

        int recordsTotal = await customerTable.CountAsync(cancellationToken);

        var customers = await customerTable
            .TagWith("Lists customer records by paging.")
            .Include(customer => customer.Device)
            .Skip(pageIndex)
            .Take(pageSize)
            .Select(customer => new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                IsBlocked = customer.IsBlocked,
                IsDeleted = customer.IsDeleted,
                Device = new Device
                {
                    CloudMessagePlatform = customer.Device.CloudMessagePlatform
                }
            })
            .ToListAsync(cancellationToken);

        return (recordsTotal, customers);
    }

    public async Task<bool> IsExistsByPhoneNumberOrEmailAsync(string phoneNumber, string email, CancellationToken cancellationToken)
    {
        return await TableNoTracking
            .TagWith("The existence of the customer's phone number or e-mail address is checked.")
            .AnyAsync(p => p.PhoneNumber == phoneNumber || p.Email == email, cancellationToken);
    }

    public async Task<List<string>> GetPhoneNumbersByIdsAsync(List<int> ids, CancellationToken cancellationToken)
    {
        return await TableNoTracking
                    .TagWith("Queries the phone numbers of the customer IDs.")
                    .Where(customer => ids.Any(id => id == customer.Id))
                    .Select(customer => customer.PhoneNumber)
                    .ToListAsync(cancellationToken);
    }

    public async Task<List<string>> GetEmailAddressesByIdsAsync(List<int> ids, CancellationToken cancellationToken)
    {
        return await TableNoTracking
                    .TagWith("Queries the phone numbers of the customer IDs.")
                    .Where(customer => ids.Any(id => id == customer.Id))
                    .Select(customer => customer.Email)
                    .ToListAsync(cancellationToken);
    }
}