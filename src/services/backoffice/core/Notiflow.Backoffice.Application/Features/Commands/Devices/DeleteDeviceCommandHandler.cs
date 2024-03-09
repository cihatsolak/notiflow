namespace Notiflow.Backoffice.Application.Features.Commands.Devices;

public sealed record DeleteDeviceCommand(int Id) : IRequest<Result<Unit>>;

public sealed class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<DeleteDeviceCommandHandler> _logger;

    public DeleteDeviceCommandHandler(
        INotiflowUnitOfWork uow,
        ILogger<DeleteDeviceCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await _uow.DeviceWrite.ExecuteDeleteByIdAsync(request.Id, cancellationToken);
        if (!isDeleted)
        {
            return Result<Unit>.Status500InternalServerError(ResultCodes.DEVICE_NOT_DELETED);
        }

        _logger.LogInformation("The device with ID {deviceId} has been deleted.", request.Id);

        return Result<Unit>.Status204NoContent(ResultCodes.DEVICE_DELETED);
    }
}

public sealed class DeleteDeviceCommandValidator : AbstractValidator<DeleteDeviceCommand>
{
    public DeleteDeviceCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}