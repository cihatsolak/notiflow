namespace Notiflow.Backoffice.Persistence.Repositories.Customers;

public sealed class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }

    public async Task<Customer> GetCustomerByPhoneNumberOrEmailAsync(string phoneNumber, string email, CancellationToken cancellationToken = default)
    {
        return await TableNoTracking.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber || p.Email == email, cancellationToken);
    }
}