namespace Notiflow.Backoffice.Application.Features.Commands.Devices;

public sealed record UpdateDeviceCommand(
    int Id,
    OSVersion OSVersion,
    string Code,
    string Token,
    CloudMessagePlatform CloudMessagePlatform
    )
    : IRequest<Result>;

public sealed class UpdateDeviceCommandHandler(
    INotiflowUnitOfWork uow,
    ILogger<UpdateDeviceCommandHandler> logger) : IRequestHandler<UpdateDeviceCommand, Result>
{
    public async Task<Result> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Result.Status404NotFound(ResultCodes.DEVICE_NOT_FOUND);
        }

        ObjectMapper.Mapper.Map(request, device);

        await uow.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Device information updated. Device ID: {deviceId}", request.Id);

        return Result.Status204NoContent(ResultCodes.DEVICE_UPDATED);
    }
}

public sealed class UpdateDeviceCommandValidator : AbstractValidator<UpdateDeviceCommand>
{
    private const int DEVICE_CODE_MAX_LENGTH = 100;
    private const int DEVICE_TOKEN_MAX_LENGTH = 100;

    public UpdateDeviceCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
        RuleFor(p => p.OSVersion).Enum(localizer[ValidationErrorMessage.OS_VERSION]);
        RuleFor(p => p.Code).Ensure(localizer[ValidationErrorMessage.DEVICE_CODE], DEVICE_CODE_MAX_LENGTH);
        RuleFor(p => p.Token).Ensure(localizer[ValidationErrorMessage.DEVICE_TOKEN], DEVICE_TOKEN_MAX_LENGTH);
        RuleFor(p => p.CloudMessagePlatform).Enum(localizer[ValidationErrorMessage.CLOUD_MESSAGE_PLATFORM]);
    }
}
