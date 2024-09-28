namespace Notiflow.Backoffice.Application.Features.Commands.Devices;

public sealed record UpdateDeviceTokenCommand(int Id, string Token) : IRequest<Result>;

public sealed class UpdateDeviceTokenCommandHandler(
    INotiflowUnitOfWork uow,
    ILogger<UpdateDeviceTokenCommandHandler> logger) : IRequestHandler<UpdateDeviceTokenCommand, Result>
{
    public async Task<Result> Handle(UpdateDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var device = await uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Result.Status404NotFound(ResultCodes.DEVICE_NOT_FOUND);
        }

        device.Token = request.Token;

        await uow.SaveChangesAsync(cancellationToken);

        logger.LogInformation("The token information of the device with {deviceId} ids has been updated.", request.Id);

        return Result.Status204NoContent();
    }
}

public sealed class UpdateDeviceTokenCommandValidator : AbstractValidator<UpdateDeviceTokenCommand>
{
    public UpdateDeviceTokenCommandValidator()
    {
        RuleFor(p => p.Id).Id(FluentVld.Errors.ID_NUMBER);
        RuleFor(p => p.Token).Ensure(FluentVld.Errors.DEVICE_TOKEN, FluentVld.Rules.DEVICE_TOKEN_MAX_100_LENGTH);
    }
}