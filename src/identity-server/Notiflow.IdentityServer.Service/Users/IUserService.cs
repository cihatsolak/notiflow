namespace Notiflow.IdentityServer.Service.Users;

public interface IUserService
{
    Task<ApiResponse<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken);
    Task<ApiResponse<int>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<EmptyResponse>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<EmptyResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<ApiResponse<string>> UpdateProfilePhotoByIdAsync(int id, IFormFile profilePhoto, CancellationToken cancellationToken);
}
