using Notiflow.IdentityServer.Service.Models;

namespace Notiflow.IdentityServer.Service.Users
{
    public interface IUserService
    {
        Task<ResponseData<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken);
        Task<ResponseData<int>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken);
        Task<ResponseData<int>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken);
        Task<ResponseData<int>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
