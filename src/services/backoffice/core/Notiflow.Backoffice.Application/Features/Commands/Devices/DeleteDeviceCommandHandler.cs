namespace Notiflow.Backoffice.Application.Features.Commands.Devices;

public sealed record DeleteDeviceCommand(int Id) : IRequest<Result>;

public sealed class DeleteDeviceCommandHandler(
    INotiflowUnitOfWork uow,
    ILogger<DeleteDeviceCommandHandler> logger) : IRequestHandler<DeleteDeviceCommand, Result>
{
    public async Task<Result> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await uow.DeviceWrite.ExecuteDeleteByIdAsync(request.Id, cancellationToken);
        if (!isDeleted)
        {
            return Result.Status500InternalServerError(ResultCodes.DEVICE_NOT_DELETED);
        }

        logger.LogInformation("The device with ID {deviceId} has been deleted.", request.Id);

        return Result.Status204NoContent(ResultCodes.DEVICE_DELETED);
    }
}

public sealed class DeleteDeviceCommandValidator : AbstractValidator<DeleteDeviceCommand>
{
    public DeleteDeviceCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}