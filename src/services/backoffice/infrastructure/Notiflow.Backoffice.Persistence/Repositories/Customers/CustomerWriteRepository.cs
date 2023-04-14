namespace Notiflow.Backoffice.Persistence.Repositories.Customers;

public sealed class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(NotiflowDbContext notiflowDbContext) : base(notiflowDbContext)
    {
    }
}