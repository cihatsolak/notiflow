namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;

public sealed class DeleteDeviceCommandValidator : AbstractValidator<DeleteDeviceCommand>
{
    public DeleteDeviceCommandValidator()
    {
        RuleFor(p => p.Id).Id(FluentValidationErrorCodes.ID_NUMBER);
    }
}
