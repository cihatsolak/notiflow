namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Update;

public sealed class UpdateDeviceCommandValidator : AbstractValidator<UpdateDeviceCommand>
{
    public UpdateDeviceCommandValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorCodes.ID_NUMBER]);

        RuleFor(p => p.OSVersion).Enum(localizer[ValidationErrorCodes.OS_VERSION]);

        RuleFor(p => p.Code)
            .NotNullAndNotEmpty(localizer[ValidationErrorCodes.DEVICE_CODE])
            .MaximumLength(100).WithMessage(localizer[ValidationErrorCodes.DEVICE_CODE]);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(localizer[ValidationErrorCodes.DEVICE_TOKEN])
            .MaximumLength(180).WithMessage(localizer[ValidationErrorCodes.DEVICE_TOKEN]);

        RuleFor(p => p.CloudMessagePlatform).Enum(localizer[ValidationErrorCodes.CLOUD_MESSAGE_PLATFORM]);
    }
}