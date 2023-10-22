namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Add;

public sealed class AddDeviceCommandValidator : AbstractValidator<AddDeviceCommand>
{
    public AddDeviceCommandValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.CustomerId).Id(localizer[ResultState.CUSTOMER_ID]);

        RuleFor(p => p.OSVersion).Enum(localizer[ResultState.OS_VERSION]);

        RuleFor(p => p.Code)
            .NotNullAndNotEmpty(localizer[ResultState.DEVICE_CODE])
            .MaximumLength(100).WithMessage(localizer[ResultState.DEVICE_CODE]);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(localizer[ResultState.DEVICE_TOKEN])
            .MaximumLength(180).WithMessage(localizer[ResultState.DEVICE_CODE]);

        RuleFor(p => p.CloudMessagePlatform).Enum(localizer[ResultState.CLOUD_MESSAGE_PLATFORM]);
    }
}
