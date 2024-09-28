namespace Notiflow.IdentityServer.Service.Models;

public sealed record UpdateUserRequest(string Name, string Surname, string Email, string Username, IFormFile Avatar);

public sealed class UpdateUserRequestExample : IExamplesProvider<UpdateUserRequest>
{
    public UpdateUserRequest GetExamples()
    {
        // Mock bir IFormFile nesnesi oluştur
        var fileName = "avatar.png";
        var content = "This is a dummy file content";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        IFormFile avatar = new FormFile(stream, 0, stream.Length, "Avatar", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };

        return new UpdateUserRequest("John", "Doe", "john.doe@example.com", "StarryTraveler92", avatar);
    }
}

public sealed class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(p => p.Name)
           .Ensure(FluentVld.Errors.USER_NAME)
           .Length(2, 100).WithMessage(FluentVld.Errors.USER_NAME);

        RuleFor(p => p.Surname)
            .Ensure(FluentVld.Errors.USER_SURNAME)
            .Length(2, 100).WithMessage(FluentVld.Errors.USER_SURNAME);

        RuleFor(p => p.Email)
            .Email(FluentVld.Errors.EMAIL)
            .Length(5, 150).WithMessage(FluentVld.Errors.EMAIL);

        RuleFor(p => p.Username)
            .Ensure(FluentVld.Errors.USERNAME)
            .Length(5, 100).WithMessage(FluentVld.Errors.USERNAME);

        RuleFor(p => p.Avatar)
            .FormFile(FluentVld.Errors.FILE, ContentTypes.ImageJpeg, ContentTypes.ImagePng);
    }
}