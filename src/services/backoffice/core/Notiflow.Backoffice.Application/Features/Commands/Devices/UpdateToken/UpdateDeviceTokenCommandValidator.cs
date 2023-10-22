namespace Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;

public sealed class UpdateDeviceTokenCommandValidator : AbstractValidator<UpdateDeviceTokenCommand>
{
    public UpdateDeviceTokenCommandValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(localizer[ResultState.DEVICE_TOKEN])
            .MaximumLength(180).WithMessage(localizer[ResultState.DEVICE_TOKEN]);
    }
}