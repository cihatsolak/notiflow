namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Send;

public sealed class SendTextMessageCommandValidator : AbstractValidator<SendTextMessageCommand>
{
    public SendTextMessageCommandValidator()
    {
        RuleForEach(p => p.CustomerIds).InclusiveBetween(1, int.MaxValue).WithMessage("-1");
        RuleFor(p => p.Message).NotNullAndNotEmpty("-1");
    }
}