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
    public DeviceInputValidator()
    {
        RuleFor(p => p.Id).Id(FluentVld.Errors.CUSTOMER_ID);

        RuleFor(p => p.CustomerId).Id(FluentVld.Errors.CUSTOMER_ID);

        RuleFor(p => p.OSVersion).Enum(FluentVld.Errors.OS_VERSION);

        RuleFor(p => p.Code)
            .Ensure(FluentVld.Errors.DEVICE_CODE)
            .MaximumLength(100).WithMessage(FluentVld.Errors.DEVICE_CODE);

        RuleFor(p => p.Token)
            .Ensure(FluentVld.Errors.DEVICE_TOKEN)
            .MaximumLength(180).WithMessage(FluentVld.Errors.DEVICE_CODE);

        RuleFor(p => p.CloudMessagePlatform).Enum(FluentVld.Errors.CLOUD_MESSAGE_PLATFORM);
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