using Notiflow.IdentityServer.Service.Models;

namespace Notiflow.IdentityServer.Service.Users
{
    public interface IUserService
    {
        Task<ResponseModel<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken);
        Task<ResponseModel<int>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken);
        Task<ResponseModel<int>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken);
        Task<ResponseModel<int>> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
