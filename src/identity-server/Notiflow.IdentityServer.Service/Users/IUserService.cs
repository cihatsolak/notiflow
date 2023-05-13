namespace Notiflow.IdentityServer.Service.Users;

public interface IUserService
{
    Task<Response<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken);
    Task<Response<int>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken);
    Task<Response<EmptyResponse>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken);
    Task<Response<EmptyResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
}
