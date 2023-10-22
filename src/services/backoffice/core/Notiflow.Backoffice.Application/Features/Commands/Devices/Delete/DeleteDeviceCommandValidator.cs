namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;

public sealed class DeleteDeviceCommandValidator : AbstractValidator<DeleteDeviceCommand>
{
    public DeleteDeviceCommandValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);
    }
}
