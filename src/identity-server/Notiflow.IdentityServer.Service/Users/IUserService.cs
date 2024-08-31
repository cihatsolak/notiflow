namespace Notiflow.IdentityServer.Service.Users;

public interface IUserService
{
    Task<Result<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken);
    Task<Result<int>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<string>> UpdateProfilePhotoByIdAsync(int id, IFormFile profilePhoto, CancellationToken cancellationToken);
}
