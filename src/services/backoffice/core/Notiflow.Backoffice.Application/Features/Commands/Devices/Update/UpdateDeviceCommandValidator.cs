namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Update;

public sealed class UpdateDeviceCommandValidator : AbstractValidator<UpdateDeviceCommand>
{
    public UpdateDeviceCommandValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);

        RuleFor(p => p.OSVersion).Enum(localizer[ResultState.OS_VERSION]);

        RuleFor(p => p.Code)
            .NotNullAndNotEmpty(localizer[ResultState.DEVICE_CODE])
            .MaximumLength(100).WithMessage(localizer[ResultState.DEVICE_CODE]);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(localizer[ResultState.DEVICE_TOKEN])
            .MaximumLength(180).WithMessage(localizer[ResultState.DEVICE_TOKEN]);

        RuleFor(p => p.CloudMessagePlatform).Enum(localizer[ResultState.CLOUD_MESSAGE_PLATFORM]);
    }
}