namespace Notiflow.Backoffice.Application.Features.Commands.Devices;

public sealed record UpdateDeviceTokenCommand(int Id, string Token) : IRequest<Result<EmptyResponse>>;

public sealed class UpdateDeviceTokenCommandHandler(
    INotiflowUnitOfWork uow,
    ILogger<UpdateDeviceTokenCommandHandler> logger) : IRequestHandler<UpdateDeviceTokenCommand, Result<EmptyResponse>>
{
    public async Task<Result<EmptyResponse>> Handle(UpdateDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var device = await uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Result<EmptyResponse>.Status404NotFound(ResultCodes.DEVICE_NOT_FOUND);
        }

        device.Token = request.Token;

        await uow.SaveChangesAsync(cancellationToken);

        logger.LogInformation("The token information of the device with {deviceId} ids has been updated.", request.Id);

        return Result<EmptyResponse>.Status204NoContent(ResultCodes.DEVICE_TOKEN_UPDATED);
    }
}

public sealed class UpdateDeviceTokenCommandValidator : AbstractValidator<UpdateDeviceTokenCommand>
{
    private const int DEVICE_TOKEN_MAX_LENGTH = 100;

    public UpdateDeviceTokenCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
        RuleFor(p => p.Token).Ensure(localizer[ValidationErrorMessage.DEVICE_TOKEN], DEVICE_TOKEN_MAX_LENGTH);
    }
}