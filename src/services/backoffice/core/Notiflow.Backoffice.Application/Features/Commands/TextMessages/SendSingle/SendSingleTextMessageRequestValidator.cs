namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendSingle;

public sealed class SendSingleTextMessageRequestValidator : AbstractValidator<SendSingleTextMessageRequest>
{
    public SendSingleTextMessageRequestValidator()
    {
        RuleFor(p => p.CustomerId).InclusiveBetween(1, int.MaxValue).WithMessage("-1");
        RuleFor(p => p.Message).NotNullAndNotEmpty("-1");
    }
}