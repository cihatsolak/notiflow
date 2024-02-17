namespace Notiflow.Backoffice.Application.Features.Commands.Devices;

public sealed record UpdateDeviceTokenCommand(int Id, string Token) : IRequest<Result<Unit>>;

public sealed class UpdateDeviceTokenCommandHandler : IRequestHandler<UpdateDeviceTokenCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateDeviceTokenCommandHandler> _logger;

    public UpdateDeviceTokenCommandHandler(
        INotiflowUnitOfWork uow,
        ILogger<UpdateDeviceTokenCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Result<Unit>.Status404NotFound(ResultCodes.DEVICE_NOT_FOUND);
        }

        device.Token = request.Token;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The token information of the device with {deviceId} ids has been updated.", request.Id);

        return Result<Unit>.Status204NoContent(ResultCodes.DEVICE_TOKEN_UPDATED);
    }
}

public sealed class UpdateDeviceTokenCommandValidator : AbstractValidator<UpdateDeviceTokenCommand>
{
    public UpdateDeviceTokenCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.DEVICE_TOKEN])
            .MaximumLength(180).WithMessage(localizer[ValidationErrorMessage.DEVICE_TOKEN]);
    }
}