namespace Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;

public sealed class UpdateDeviceTokenCommandValidator : AbstractValidator<UpdateDeviceTokenCommand>
{
    public UpdateDeviceTokenCommandValidator()
    {
        RuleFor(p => p.Id).Id(FluentValidationErrorCodes.ID_NUMBER);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(FluentValidationErrorCodes.DEVICE_TOKEN)
            .MaximumLength(180).WithMessage(FluentValidationErrorCodes.DEVICE_TOKEN);
    }
}