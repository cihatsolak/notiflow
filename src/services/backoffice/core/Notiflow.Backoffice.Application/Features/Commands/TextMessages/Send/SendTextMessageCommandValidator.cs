namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Send;

public sealed class SendTextMessageCommandValidator : AbstractValidator<SendTextMessageCommand>
{
    public SendTextMessageCommandValidator()
    {
        RuleForEach(p => p.CustomerIds)
           .InclusiveBetween(1, int.MaxValue).WithMessage(FluentValidationErrorCodes.CUSTOMER_ID);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(FluentValidationErrorCodes.TEXT_MESSAGE)
           .MaximumLength(300).WithMessage(FluentValidationErrorCodes.TEXT_MESSAGE);
    }
}