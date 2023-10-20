using Notiflow.Common.Localize;

namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Add;

public sealed class AddDeviceCommandValidator : AbstractValidator<AddDeviceCommand>
{
    public AddDeviceCommandValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleFor(p => p.CustomerId).Id(localizer[ValidationErrorCodes.CUSTOMER_ID]);

        RuleFor(p => p.OSVersion).Enum(localizer[ValidationErrorCodes.OS_VERSION]);

        RuleFor(p => p.Code)
            .NotNullAndNotEmpty(localizer[ValidationErrorCodes.DEVICE_CODE])
            .MaximumLength(100).WithMessage(localizer[ValidationErrorCodes.DEVICE_CODE]);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(localizer[ValidationErrorCodes.DEVICE_TOKEN])
            .MaximumLength(180).WithMessage(localizer[ValidationErrorCodes.DEVICE_CODE]);

        RuleFor(p => p.CloudMessagePlatform).Enum(localizer[ValidationErrorCodes.CLOUD_MESSAGE_PLATFORM]);
    }
}
