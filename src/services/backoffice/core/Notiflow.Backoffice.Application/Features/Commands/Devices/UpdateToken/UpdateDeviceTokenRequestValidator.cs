namespace Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;

public sealed class UpdateDeviceTokenRequestValidator : AbstractValidator<UpdateDeviceTokenRequest>
{
    public UpdateDeviceTokenRequestValidator()
    {
        RuleFor(p => p.Id).InclusiveBetween(1, int.MaxValue).WithMessage("-1");
        RuleFor(p => p.Token).NotNullAndNotEmpty("-1").MaximumLength(180).WithMessage("-1");
    }
}