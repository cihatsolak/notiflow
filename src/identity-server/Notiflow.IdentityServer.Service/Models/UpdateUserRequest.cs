namespace Notiflow.IdentityServer.Service.Models;

public sealed record UpdateUserRequest(string Name, string Surname, string Email, string Username, IFormFile Avatar);

public sealed class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Name)
           .NotNullAndNotEmpty(localizer[ValidationErrorMessage.USER_NAME])
           .Length(2, 100).WithMessage(localizer[ValidationErrorMessage.USER_NAME]);

        RuleFor(p => p.Surname)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.USER_SURNAME])
            .Length(2, 100).WithMessage(localizer[ValidationErrorMessage.USER_SURNAME]);

        RuleFor(p => p.Email)
            .Email(localizer[ValidationErrorMessage.EMAIL])
            .Length(5, 150).WithMessage(localizer[ValidationErrorMessage.EMAIL]);

        RuleFor(p => p.Username)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.USERNAME])
            .Length(5, 100).WithMessage(localizer[ValidationErrorMessage.USERNAME]);

        RuleFor(p => p.Avatar)
            .FormFile(localizer[ValidationErrorMessage.FILE], ContentTypes.ImageJpeg, ContentTypes.ImagePng);
    }
}
