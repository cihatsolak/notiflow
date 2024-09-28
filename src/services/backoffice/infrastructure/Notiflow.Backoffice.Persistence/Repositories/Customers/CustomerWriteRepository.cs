namespace Notiflow.Backoffice.Persistence.Repositories.Customers;

public sealed class CustomerWriteRepository(NotiflowDbContext notiflowDbContext) 
    : WriteRepository<Customer>(notiflowDbContext), ICustomerWriteRepository
{
}