namespace Notiflow.Panel.Models;

public sealed record NotificationInput
{
    [Display(Name = "input.customer.numbers.resource")]
    [DataType(DataType.Text)]
    public required List<int> CustomerIds { get; init; }

    [Display(Name = "input.title.resource")]
    [DataType(DataType.Text)]
    public required string Title { get; init; }

    [Display(Name = "input.message.resource")]
    [DataType(DataType.MultilineText)]
    public required string Message { get; init; }

    [Display(Name = "input.notification.imageurl.resource")]
    [DataType(DataType.ImageUrl)]
    public required string ImageUrl { get; init; }
}

public sealed class NotificationInputValidator : AbstractValidator<NotificationInput>
{
    public NotificationInputValidator()
    {
        RuleForEach(p => p.CustomerIds).Id(FluentVld.Errors.CUSTOMER_ID);

        RuleFor(p => p.Title)
            .Ensure(FluentVld.Errors.NOTIFICATION_TITLE)
            .MaximumLength(300).WithMessage(FluentVld.Errors.NOTIFICATION_TITLE);

        RuleFor(p => p.Message)
           .Ensure(FluentVld.Errors.NOTIFICATION_MESSAGE)
           .MaximumLength(300).WithMessage(FluentVld.Errors.NOTIFICATION_MESSAGE);

        RuleFor(p => p.ImageUrl)
         .Ensure(FluentVld.Errors.NOTIFICATION_IMAGE_URL)
         .MaximumLength(300).WithMessage(FluentVld.Errors.NOTIFICATION_IMAGE_URL)
         .Must(BeAValidUrl).WithMessage(FluentVld.Errors.NOTIFICATION_IMAGE_URL);
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}