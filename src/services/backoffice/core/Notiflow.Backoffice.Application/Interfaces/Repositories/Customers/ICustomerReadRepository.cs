namespace Notiflow.Backoffice.Application.Interfaces.Repositories.Customers;

public interface ICustomerReadRepository : IReadRepository<Customer>
{
    Task<bool> IsExistsByPhoneNumberOrEmailAsync(string phoneNumber, string email, CancellationToken cancellationToken = default);
    Task<List<string>> GetPhoneNumbersByIdsAsync(List<int> ids, CancellationToken cancellationToken = default);
    Task<List<string>> GetEmailAddressesByIdsAsync(List<int> ids, CancellationToken cancellationToken = default);
}
