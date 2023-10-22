namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Send;

public sealed class SendTextMessageCommandValidator : AbstractValidator<SendTextMessageCommand>
{
    public SendTextMessageCommandValidator(ILocalizerService<ResultState> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ResultState.CUSTOMER_ID]);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(localizer[ResultState.TEXT_MESSAGE])
           .MaximumLength(300).WithMessage(localizer[ResultState.TEXT_MESSAGE]);
    }
}