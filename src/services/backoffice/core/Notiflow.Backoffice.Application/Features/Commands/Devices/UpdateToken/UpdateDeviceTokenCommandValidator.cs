namespace Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;

public sealed class UpdateDeviceTokenCommandValidator : AbstractValidator<UpdateDeviceTokenCommand>
{
    public UpdateDeviceTokenCommandValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorCodes.ID_NUMBER]);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(localizer[ValidationErrorCodes.DEVICE_TOKEN])
            .MaximumLength(180).WithMessage(localizer[ValidationErrorCodes.DEVICE_TOKEN]);
    }
}