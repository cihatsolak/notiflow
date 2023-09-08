namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Update;

public sealed class UpdateDeviceCommandValidator : AbstractValidator<UpdateDeviceCommand>
{
    public UpdateDeviceCommandValidator()
    {
        RuleFor(p => p.Id).Id(FluentValidationErrorCodes.ID_NUMBER);

        RuleFor(p => p.OSVersion).Enum(FluentValidationErrorCodes.OS_VERSION);

        RuleFor(p => p.Code)
            .NotNullAndNotEmpty(FluentValidationErrorCodes.DEVICE_CODE)
            .MaximumLength(100).WithMessage(FluentValidationErrorCodes.DEVICE_CODE);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(FluentValidationErrorCodes.DEVICE_TOKEN)
            .MaximumLength(180).WithMessage(FluentValidationErrorCodes.DEVICE_TOKEN);

        RuleFor(p => p.CloudMessagePlatform).Enum(FluentValidationErrorCodes.CLOUD_MESSAGE_PLATFORM);
    }
}