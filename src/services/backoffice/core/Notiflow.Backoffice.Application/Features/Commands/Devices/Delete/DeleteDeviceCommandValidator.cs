namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;

public sealed class DeleteDeviceCommandValidator : AbstractValidator<DeleteDeviceCommand>
{
    public DeleteDeviceCommandValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorCodes.ID_NUMBER]);
    }
}
