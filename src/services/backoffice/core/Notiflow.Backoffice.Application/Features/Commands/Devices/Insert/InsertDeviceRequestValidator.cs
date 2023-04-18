namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Insert;

public sealed class InsertDeviceRequestValidator : AbstractValidator<InsertDeviceRequest>
{
    public InsertDeviceRequestValidator()
    {
        RuleFor(p => p.CustomerId).InclusiveBetween(1, int.MaxValue).WithMessage("-1");
        RuleFor(p => p.OSVersion).IsInEnum().WithMessage("-1");
        RuleFor(p => p.Code).NotNullAndNotEmpty("-1").MaximumLength(100).WithMessage("-1");
        RuleFor(p => p.Token).NotNullAndNotEmpty("-1").MaximumLength(180).WithMessage("-1");
        RuleFor(p => p.CloudMessagePlatform).IsInEnum().WithMessage("-1");
    }
}
