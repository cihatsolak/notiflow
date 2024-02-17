namespace Notiflow.Backoffice.Application.Features.Commands.Devices;

public sealed record UpdateDeviceCommand(
    int Id,
    OSVersion OSVersion,
    string Code,
    string Token,
    CloudMessagePlatform CloudMessagePlatform
    )
    : IRequest<Result<Unit>>;

public sealed class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateDeviceCommandHandler> _logger;

    public UpdateDeviceCommandHandler(
        INotiflowUnitOfWork uow,
        ILogger<UpdateDeviceCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Result<Unit>.Status404NotFound(ResultCodes.DEVICE_NOT_FOUND);
        }

        ObjectMapper.Mapper.Map(request, device);

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Device information updated. Device ID: {deviceId}", request.Id);

        return Result<Unit>.Status204NoContent(ResultCodes.DEVICE_UPDATED);
    }
}

public sealed class UpdateDeviceCommandValidator : AbstractValidator<UpdateDeviceCommand>
{
    public UpdateDeviceCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);

        RuleFor(p => p.OSVersion).Enum(localizer[ValidationErrorMessage.OS_VERSION]);

        RuleFor(p => p.Code)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.DEVICE_CODE])
            .MaximumLength(100).WithMessage(localizer[ValidationErrorMessage.DEVICE_CODE]);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.DEVICE_TOKEN])
            .MaximumLength(180).WithMessage(localizer[ValidationErrorMessage.DEVICE_TOKEN]);

        RuleFor(p => p.CloudMessagePlatform).Enum(localizer[ValidationErrorMessage.CLOUD_MESSAGE_PLATFORM]);
    }
}
