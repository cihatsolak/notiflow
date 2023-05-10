using Notiflow.IdentityServer.Service.Models.Users;

namespace Notiflow.IdentityServer.Service.Users;

public interface IUserService
{
    Task<ResponseData<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken);
    Task<Response> AddAsync(CreateUserRequest request, CancellationToken cancellationToken);
    Task<Response> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken);
    Task<Response> DeleteAsync(int id, CancellationToken cancellationToken);
}
