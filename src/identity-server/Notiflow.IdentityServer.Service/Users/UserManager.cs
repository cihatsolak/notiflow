﻿using Notiflow.IdentityServer.Service.Infrastructure;

namespace Notiflow.IdentityServer.Service.Users;

internal sealed class UserManager : IUserService
{
    private const string PLACEHOLDER_AVATAR_URL = "https://via.placeholder.com/250";

    private readonly ApplicationDbContext _context;
    private readonly IFtpService _fileService;
    private readonly IClaimService _claimService;
    private readonly ILocalizerService<ResultMessage> _localizer;
    private readonly ILogger<UserManager> _logger;

    public UserManager(
        ApplicationDbContext context, 
        IFtpService fileService,
        IClaimService claimService,
        ILocalizerService<ResultMessage> localizer,
        ILogger<UserManager> logger)
    {
        _context = context;
        _claimService = claimService;
        _fileService = fileService;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<UserResponse>> GetDetailAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            return Result<UserResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.USER_NOT_FOUND]);
        }

        return Result<UserResponse>.Success(StatusCodes.Status200OK, _localizer[ResultMessage.GENERAL_SUCCESS], user.Adapt<UserResponse>());
    }

    public async Task<Result<int>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        bool isExists = _context.Users
            .TagWith("Checks whether a user exists based on email or username.")
            .Any(p => p.Username.Equals(request.Username) || p.Email.Equals(request.Email));
        if (isExists)
        {
            return Result<int>.Failure(StatusCodes.Status400BadRequest, _localizer[ResultMessage.USER_EXISTS]);
        }

        var user = request.Adapt<User>();
        user.TenantId = int.Parse(_claimService.GroupSid);
        user.Avatar = PLACEHOLDER_AVATAR_URL;

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(StatusCodes.Status201Created, _localizer[ResultMessage.USER_ADDED], user.Id);
    }

    public async Task<Result<EmptyResponse>> UpdateAsync(int id, UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            return Result<EmptyResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.USER_NOT_FOUND]);
        }

        request.Adapt(user);

        if (request.Avatar is null || 0 >= request.Avatar.Length)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return Result<EmptyResponse>.Success(StatusCodes.Status204NoContent, _localizer[ResultMessage.USER_UPTATED]);
        }

        var fileProcessResult = await _fileService.AddAfterRenameIfAvailableAsync(request.Avatar, AppFilePaths.PROFILE_PHOTOS, cancellationToken);
        if (!fileProcessResult.Succeeded)
        {
            _logger.LogWarning("Failed to upload profile photo of user with ID {@userId}.", id);
        }
        else
        {
            user.Avatar = fileProcessResult.Url;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Result<EmptyResponse>.Success(StatusCodes.Status204NoContent, _localizer[ResultMessage.USER_UPTATED]);
    }

    public async Task<Result<EmptyResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        int numberOfRowsDeleted = await _context.Users
            .TagWith("Deletes the user based on user ID.")
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        if (numberOfRowsDeleted != 1)
        {
            return Result<EmptyResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.USER_NOT_DELETED]);
        }

        return Result<EmptyResponse>.Success(StatusCodes.Status204NoContent, _localizer[ResultMessage.USER_DELETED]);
    }

    public async Task<Result<string>> UpdateProfilePhotoByIdAsync(int id, IFormFile profilePhoto, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, cancellationToken);
        if (user is null)
        {
            return Result<string>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.USER_NOT_FOUND]);
        }
        
        var fileProcessResult = await _fileService.AddAfterRenameIfAvailableAsync(profilePhoto, AppFilePaths.PROFILE_PHOTOS, cancellationToken);
        if (!fileProcessResult.Succeeded)
        {
            _logger.LogWarning("Failed to upload profile photo of user with ID {@userId}.", id);
            return Result<string>.Failure(StatusCodes.Status500InternalServerError, _localizer[ResultMessage.USER_PROFILE_PHOTO_NOT_UPDATED]);
        }

        user.Avatar = fileProcessResult.Url;
        await _context.SaveChangesAsync(cancellationToken);

        return Result<string>.Success(StatusCodes.Status204NoContent, _localizer[ResultMessage.USER_PROFILE_PHOTO_UPDATED], user.Avatar);
    }
}
