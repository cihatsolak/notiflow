namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Add;

public sealed class AddDeviceCommandValidator : AbstractValidator<AddDeviceCommand>
{
    public AddDeviceCommandValidator()
    {
        RuleFor(p => p.CustomerId)
            .InclusiveBetween(1, int.MaxValue)
            .WithMessage(FluentValidationErrorCodes.CUSTOMER_ID);

        RuleFor(p => p.OSVersion).Enum(FluentValidationErrorCodes.OS_VERSION);

        RuleFor(p => p.Code)
            .NotNullAndNotEmpty(FluentValidationErrorCodes.DEVICE_CODE)
            .MaximumLength(100).WithMessage(FluentValidationErrorCodes.DEVICE_CODE);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(FluentValidationErrorCodes.DEVICE_TOKEN)
            .MaximumLength(180).WithMessage(FluentValidationErrorCodes.DEVICE_CODE);

        RuleFor(p => p.CloudMessagePlatform).Enum(FluentValidationErrorCodes.CLOUD_MESSAGE_PLATFORM);
    }
}
