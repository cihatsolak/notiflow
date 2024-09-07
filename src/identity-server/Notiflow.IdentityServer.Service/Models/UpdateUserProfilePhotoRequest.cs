namespace Notiflow.IdentityServer.Service.Models;

/// <summary>
/// Represents a request to update the user profile photo.
/// </summary>
public sealed record UpdateUserProfilePhotoRequest(int Id, IFormFile ProfilePhoto);

/// <summary>
/// Provides an example of the <see cref="UpdateUserProfilePhotoRequest"/> class.
/// </summary>
public sealed class UpdateUserProfilePhotoRequestExample : IExamplesProvider<UpdateUserProfilePhotoRequest>
{
    /// <summary>
    /// Gets an example of the <see cref="UpdateUserProfilePhotoRequest"/> class.
    /// </summary>
    /// <returns>An example of the <see cref="UpdateUserProfilePhotoRequest"/> class.</returns>
    public UpdateUserProfilePhotoRequest GetExamples()
    {
        // Mock an IFormFile object
        var fileName = "profile-photo.png";
        var content = "This is a dummy file content";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        IFormFile profilePhoto = new FormFile(stream, 0, stream.Length, "ProfilePhoto", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };

        return new UpdateUserProfilePhotoRequest(22452, profilePhoto);
    }
}

/// <summary>
/// Provides validation rules for the <see cref="UpdateUserProfilePhotoRequest"/> class.
/// </summary>
public sealed class UpdateUserProfilePhotoRequestValidator : AbstractValidator<UpdateUserProfilePhotoRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserProfilePhotoRequestValidator"/> class.
    /// </summary>
    /// <param name="localizer">The localizer service for validation error messages.</param>
    public UpdateUserProfilePhotoRequestValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0).WithMessage(FluentVld.Errors.ID_NUMBER);

        RuleFor(p => p.ProfilePhoto)
            .FormFile(FluentVld.Errors.FILE, ContentTypes.ImageJpeg, ContentTypes.ImagePng);
    }
}
