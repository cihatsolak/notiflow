namespace Notiflow.Backoffice.Persistence.Repositories.Customers;

public sealed class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }

    public async Task<bool> IsExistsByPhoneNumberOrEmailAsync(string phoneNumber, string email, CancellationToken cancellationToken = default)
    {
        return await TableNoTracking
            .TagWith("The existence of the customer's phone number or e-mail address is checked.")
            .AnyAsync(p => p.PhoneNumber == phoneNumber || p.Email == email, cancellationToken);
    }

    public async Task<string> GetPhoneNumberByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await TableNoTracking
               .TagWith("Queries the customer's phone number.")
               .Where(customer => customer.Id == id)
               .Select(customer => customer.PhoneNumber)
               .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Customer>> GetPhoneNumbersByIdsAsync(List<int> ids, CancellationToken cancellationToken = default)
    {
        return await TableNoTracking
                    .TagWith("Queries the phone numbers of the customer IDs")
                    .Where(customer => ids.Any(id => id == customer.Id))
                    .Select(customer => new Customer
                    {
                        Id = customer.Id,
                        PhoneNumber = customer.PhoneNumber
                    })
                    .ToListAsync(cancellationToken);
    }
}