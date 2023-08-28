﻿namespace Notiflow.IdentityServer.Service.Users;

internal sealed class UserManager : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IFileService _fileService;
    private readonly ILogger<UserManager> _logger;

    public UserManager(
        ApplicationDbContext context, 
        IFileService fileService,
        ILogger<UserManager> logger)
    {
        _context = context;
        _fileService = fileService;
        _logger = logger;
    }

    public async Task<Response<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("User not found by ID {@userId}.", id);
            return Response<UserResponse>.Fail(-1);
        }

        return Response<UserResponse>.Success(user.Adapt<UserResponse>());
    }

    public async Task<Response<int>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        bool isExists = _context.Users.Any(p => p.Username.Equals(request.Username) || p.Email.Equals(request.Email));
        if (isExists)
        {
            _logger.LogInformation("There is an existing user by username or e-mail.");
            return Response<int>.Fail(-1);
        }

        var user = request.Adapt<User>();
        user.TenantId = 1;

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Response<int>.Success(1);
    }

    public async Task<Response<EmptyResponse>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("User not found by ID {@userId}.", id);
            return Response<EmptyResponse>.Fail(-1);
        }

        request.Adapt(user);

        if (request.Avatar is null || 0 >= request.Avatar.Length)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return Response<EmptyResponse>.Success(1);
        }

        var fileProcessResult = await _fileService.AddAfterRenameIfAvailableAsync(request.Avatar, FilePaths.PROFILE_PHOTOS, cancellationToken);
        if (!fileProcessResult.Succeeded)
        {
            _logger.LogWarning("Failed to upload profile photo of user with ID {@userId}.", id);
        }
        else
        {
            user.Avatar = fileProcessResult.Url;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Response<EmptyResponse>.Success(1);
    }

    public async Task<Response<EmptyResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        int numberOfRowsDeleted = await _context.Users.Where(p => p.Id == id).ExecuteDeleteAsync(cancellationToken);
        if (numberOfRowsDeleted != 1)
        {
            _logger.LogInformation("Could not delete user of ID {@userId}.", id);
            return Response<EmptyResponse>.Fail(-1);
        }

        return Response<EmptyResponse>.Success(1);
    }

    public async Task<Response<string>> UpdateProfilePhotoByIdAsync(int id, IFormFile profilePhoto, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("User not found by ID {@userId}.", id);
            return Response<string>.Fail(-1);
        }
        
        var fileProcessResult = await _fileService.AddAfterRenameIfAvailableAsync(profilePhoto, FilePaths.PROFILE_PHOTOS, cancellationToken);
        if (!fileProcessResult.Succeeded)
        {
            _logger.LogWarning("Failed to upload profile photo of user with ID {@userId}.", id);
            return Response<string>.Fail(-1);
        }

        user.Avatar = fileProcessResult.Url;
        await _context.SaveChangesAsync(cancellationToken);

        return Response<string>.Success(user.Avatar);
    }
}
