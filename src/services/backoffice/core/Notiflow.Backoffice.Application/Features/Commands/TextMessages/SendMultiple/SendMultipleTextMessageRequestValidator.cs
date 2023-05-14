namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendMultiple;

public sealed class SendMultipleTextMessageRequestValidator : AbstractValidator<SendMultipleTextMessageRequest>
{
    public SendMultipleTextMessageRequestValidator()
    {
        RuleForEach(p => p.CustomerIds).InclusiveBetween(1, int.MaxValue).WithMessage("-1");
        RuleFor(p => p.Message).NotNullAndNotEmpty("-1");
    }
}