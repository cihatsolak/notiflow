namespace Notiflow.Backoffice.Application.Interfaces.Repositories.Customers;

public interface ICustomerReadRepository : IReadRepository<Customer>
{
    Task<Customer> GetCustomerByPhoneNumberOrEmailAsync(string phoneNumber, string email, CancellationToken cancellationToken = default);
}
