namespace Notiflow.Panel.Models;

public sealed record TextMessageInput
{
    public TextMessageInput()
    {
        SelectedCustomerIds = new();
        AvailableCustomers = new List<SelectListItem>();
    }

    public string Message { get; init; }
    public List<int> SelectedCustomerIds { get; init; }
    public IList<SelectListItem> AvailableCustomers { get; init; }
}

public sealed class TextMessageInputValidator : AbstractValidator<TextMessageInput>
{
    public TextMessageInputValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.SelectedCustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(localizer[ValidationErrorMessage.TEXT_MESSAGE])
           .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.TEXT_MESSAGE]);
    }
}