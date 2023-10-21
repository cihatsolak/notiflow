namespace Notiflow.IdentityServer.Service.Users;

internal sealed class UserManager : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IFileService _fileService;
    private readonly ILocalizerService<ResultState> _localizer;
    private readonly ILogger<UserManager> _logger;

    public UserManager(
        ApplicationDbContext context, 
        IFileService fileService,
        ILocalizerService<ResultState> localizer,
        ILogger<UserManager> logger)
    {
        _context = context;
        _fileService = fileService;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            return Result<UserResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.USER_NOT_FOUND]);
        }

        return Result<UserResponse>.Success(StatusCodes.Status200OK, _localizer[ResultState.GENERAL_SUCCESS], user.Adapt<UserResponse>());
    }

    public async Task<Result<int>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        bool isExists = _context.Users.Any(p => p.Username.Equals(request.Username) || p.Email.Equals(request.Email));
        if (isExists)
        {
            return Result<int>.Failure(StatusCodes.Status400BadRequest, _localizer[ResultState.USER_EXISTS]);
        }

        var user = request.Adapt<User>();
        user.TenantId = 1;

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(StatusCodes.Status201Created, _localizer[ResultState.USER_ADDED], user.Id);
    }

    public async Task<Result<EmptyResponse>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            return Result<EmptyResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.USER_NOT_FOUND]);
        }

        request.Adapt(user);

        if (request.Avatar is null || 0 >= request.Avatar.Length)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return Result<EmptyResponse>.Success(StatusCodes.Status204NoContent, _localizer[ResultState.USER_UPTATED]);
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
        return Result<EmptyResponse>.Success(StatusCodes.Status204NoContent, _localizer[ResultState.USER_UPTATED]);
    }

    public async Task<Result<EmptyResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        int numberOfRowsDeleted = await _context.Users.Where(p => p.Id == id).ExecuteDeleteAsync(cancellationToken);
        if (numberOfRowsDeleted != 1)
        {
            return Result<EmptyResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.USER_NOT_DELETED]);
        }

        return Result<EmptyResponse>.Success(StatusCodes.Status204NoContent, _localizer[ResultState.USER_DELETED]);
    }

    public async Task<Result<string>> UpdateProfilePhotoByIdAsync(int id, IFormFile profilePhoto, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            return Result<string>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.USER_NOT_FOUND]);
        }
        
        var fileProcessResult = await _fileService.AddAfterRenameIfAvailableAsync(profilePhoto, FilePaths.PROFILE_PHOTOS, cancellationToken);
        if (!fileProcessResult.Succeeded)
        {
            _logger.LogWarning("Failed to upload profile photo of user with ID {@userId}.", id);
            return Result<string>.Failure(StatusCodes.Status500InternalServerError, _localizer[ResultState.USER_PROFILE_PHOTO_NOT_UPDATED]);
        }

        user.Avatar = fileProcessResult.Url;
        await _context.SaveChangesAsync(cancellationToken);

        return Result<string>.Success(StatusCodes.Status204NoContent, _localizer[ResultState.USER_PROFILE_PHOTO_UPDATED], user.Avatar);
    }
}
