namespace Notiflow.IdentityServer.Service.Users;

internal sealed class UserManager : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IFtpService _fileService;
    private readonly IClaimService _claimService;

    public UserManager(
        ApplicationDbContext context,
        IFtpService fileService,
        IClaimService claimService)
    {
        _context = context;
        _claimService = claimService;
        _fileService = fileService;
    }

    public async Task<Result<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            return Result<UserResponse>.Status404NotFound(ResultCodes.USER_NOT_FOUND);
        }

        return Result<UserResponse>.Status200OK(ResultCodes.GENERAL_SUCCESS, user.Adapt<UserResponse>());
    }

    public async Task<Result<int>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        bool isExists = await _context.Users
                .TagWith("Checks whether a user exists based on email or username.")
                .AnyAsync(p => p.Username.Equals(request.Username) || p.Email.Equals(request.Email), cancellationToken);
        if (isExists)
        {
            return Result<int>.Status400BadRequest(ResultCodes.USER_EXISTS);
        }

        var user = request.Adapt<User>();
        user.TenantId = int.Parse(_claimService.GroupSid);
        user.Avatar = AppFilePaths.PLACEHOLDER_AVATAR_URL;

        UserOutbox userOutbox = new()
        {
            IdempotentToken = Guid.NewGuid(),
            MessageType = typeof(UserRegisteredEvent).Name,
            Payload = new UserRegisteredEvent(user.Email, "Welcome!").ToJson()
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.UserOutboxes.AddAsync(userOutbox, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Status201Created(ResultCodes.USER_ADDED, user.Id);
    }

    public async Task<Result<EmptyResponse>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            return Result<EmptyResponse>.Status404NotFound(ResultCodes.USER_NOT_FOUND);
        }

        request.Adapt(user);

        if (request.Avatar is null || 0 >= request.Avatar.Length)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return Result<EmptyResponse>.Status204NoContent(ResultCodes.USER_UPTATED);
        }

        var fileResult = await _fileService.AddAfterRenameIfAvailableAsync(request.Avatar, AppFilePaths.PROFILE_PHOTOS, cancellationToken);
        if (fileResult.Succeeded)
        {
            user.Avatar = fileResult.Url;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result<EmptyResponse>.Status204NoContent(ResultCodes.USER_UPTATED);
    }

    public async Task<Result<EmptyResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        int numberOfRowsDeleted = await _context.Users
            .TagWith("Deletes the user based on user ID.")
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        if (0 >= numberOfRowsDeleted)
        {
            return Result<EmptyResponse>.Status404NotFound(ResultCodes.USER_NOT_DELETED);
        }

        return Result<EmptyResponse>.Status204NoContent(ResultCodes.USER_DELETED);
    }

    public async Task<Result<string>> UpdateProfilePhotoByIdAsync(int id, IFormFile profilePhoto, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            return Result<string>.Status404NotFound(ResultCodes.USER_NOT_FOUND);
        }

        var fileResult = await _fileService.AddAfterRenameIfAvailableAsync(profilePhoto, AppFilePaths.PROFILE_PHOTOS, cancellationToken);
        if (!fileResult.Succeeded)
        {
            return Result<string>.Status500InternalServerError(ResultCodes.USER_PROFILE_PHOTO_NOT_UPDATED);
        }

        user.Avatar = fileResult.Url;
        await _context.SaveChangesAsync(cancellationToken);

        return Result<string>.Status200OK(ResultCodes.USER_PROFILE_PHOTO_UPDATED, user.Avatar);
    }
}
