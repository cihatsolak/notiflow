namespace Notiflow.Panel.Models;

public sealed record TextMessageInput
{
    [Display(Name = "input.message.resource")]
    [DataType(DataType.MultilineText)]
    public string Message { get; init; }

    [Display(Name = "input.customer.numbers.resource")]
    [DataType(DataType.Text)]
    public List<int> CustomerIds { get; init; }
}

public sealed class TextMessageInputValidator : AbstractValidator<TextMessageInput>
{
    public TextMessageInputValidator()
    {
        RuleForEach(p => p.CustomerIds).Id(FluentVld.Errors.CUSTOMER_ID);

        RuleFor(p => p.Message)
           .Ensure(FluentVld.Errors.TEXT_MESSAGE)
           .MaximumLength(300).WithMessage(FluentVld.Errors.TEXT_MESSAGE);
    }
}