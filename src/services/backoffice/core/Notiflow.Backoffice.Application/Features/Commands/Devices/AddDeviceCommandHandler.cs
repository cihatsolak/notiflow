﻿namespace Notiflow.Backoffice.Application.Features.Commands.Devices;

public sealed record AddDeviceCommand(
    int CustomerId,
    OSVersion OSVersion,
    string Code,
    string Token,
    CloudMessagePlatform CloudMessagePlatform
    ) : IRequest<Result<int>>;

public sealed class AddDeviceCommandHandler(
    INotiflowUnitOfWork notiflowUnitOfWork,
    ILogger<AddDeviceCommandHandler> logger) : IRequestHandler<AddDeviceCommand, Result<int>>
{
    public async Task<Result<int>> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await notiflowUnitOfWork.DeviceRead.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (device is not null)
        {
            return Result<int>.Status404NotFound(ResultCodes.DEVICE_EXISTS);
        }

        device = ObjectMapper.Mapper.Map<Device>(request);
        await notiflowUnitOfWork.DeviceWrite.InsertAsync(device, cancellationToken);
        await notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("A new device with ID {deviceId} has been added for the customer with ID number {customerId}.", device.Id, device.CustomerId);

        return Result<int>.Status201Created(ResultCodes.DEVICE_ASSOCIATED_CUSTOMER_ADDED, device.Id);
    }
}

public sealed class AddDeviceCommandValidator : AbstractValidator<AddDeviceCommand>
{
    private const int DEVICE_CODE_MAX_LENGTH = 100;
    private const int DEVICE_TOKEN_MAX_LENGTH = 100;

    public AddDeviceCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.CustomerId).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);
        RuleFor(p => p.OSVersion).Enum(localizer[ValidationErrorMessage.OS_VERSION]);
        RuleFor(p => p.Code).Ensure(localizer[ValidationErrorMessage.DEVICE_CODE], DEVICE_CODE_MAX_LENGTH);
        RuleFor(p => p.Token).Ensure(localizer[ValidationErrorMessage.DEVICE_TOKEN], DEVICE_TOKEN_MAX_LENGTH);
        RuleFor(p => p.CloudMessagePlatform).Enum(localizer[ValidationErrorMessage.CLOUD_MESSAGE_PLATFORM]);
    }
}
