namespace Notiflow.Panel.Models;

public sealed record DeviceInput
{
    public int Id { get; init; }

    [Display(Name = "input.customerid.resource")]
    public int CustomerId { get; init; }

    [Display(Name = "input.device.osversion.resource")]
    public required OSVersion OSVersion { get; init; }

    [Display(Name = "input.device.code.resource")]
    public required string Code { get; init; }

    [Display(Name = "input.device.token.resource")]
    public required string Token { get; init; }

    [Display(Name = "input.device.cloudmessageplatform.resource")]
    public required CloudMessagePlatform CloudMessagePlatform { get; init; }
}

public sealed class DeviceInputValidator : AbstractValidator<DeviceInput>
{
    public DeviceInputValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        RuleFor(p => p.CustomerId).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        RuleFor(p => p.OSVersion).Enum(localizer[ValidationErrorMessage.OS_VERSION]);

        RuleFor(p => p.Code)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.DEVICE_CODE])
            .MaximumLength(100).WithMessage(localizer[ValidationErrorMessage.DEVICE_CODE]);

        RuleFor(p => p.Token)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.DEVICE_TOKEN])
            .MaximumLength(180).WithMessage(localizer[ValidationErrorMessage.DEVICE_CODE]);

        RuleFor(p => p.CloudMessagePlatform).Enum(localizer[ValidationErrorMessage.CLOUD_MESSAGE_PLATFORM]);
    }
}

public enum OSVersion : byte
{
    Android = 1,
    IOS,
    Windows,
    Other
}

public enum CloudMessagePlatform : byte
{
    Firesabe = 1,
    Huawei
}