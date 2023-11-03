namespace Notiflow.Backoffice.Application.Interfaces.Repositories.Customers;

public interface ICustomerReadRepository : IReadRepository<Customer>
{
    Task<(int recordsTotal, List<Customer> customers)> GetPageAsync(string sortKey, string searchKey, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<bool> IsExistsByPhoneNumberOrEmailAsync(string phoneNumber, string email, CancellationToken cancellationToken);
    Task<List<string>> GetPhoneNumbersByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    Task<List<string>> GetEmailAddressesByIdsAsync(List<int> ids, CancellationToken cancellationToken);
}
