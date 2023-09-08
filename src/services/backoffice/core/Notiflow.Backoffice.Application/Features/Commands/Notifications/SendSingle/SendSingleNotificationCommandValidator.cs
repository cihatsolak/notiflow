﻿namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendSingle;

public sealed class SendSingleNotificationCommandValidator : AbstractValidator<SendSingleNotificationCommand>
{
    public SendSingleNotificationCommandValidator()
    {
        RuleFor(p => p.CustomerId)
          .InclusiveBetween(1, int.MaxValue).WithMessage(FluentValidationErrorCodes.CUSTOMER_ID);

        RuleFor(p => p.Title)
            .NotNullAndNotEmpty(FluentValidationErrorCodes.NOTIFICATION_TITLE)
            .MaximumLength(300).WithMessage(FluentValidationErrorCodes.NOTIFICATION_TITLE);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(FluentValidationErrorCodes.NOTIFICATION_MESSAGE)
           .MaximumLength(300).WithMessage(FluentValidationErrorCodes.NOTIFICATION_MESSAGE);

        RuleFor(p => p.ImageUrl)
           .NotNullAndNotEmpty(FluentValidationErrorCodes.NOTIFICATION_IMAGE_URL)
           .MaximumLength(300).WithMessage(FluentValidationErrorCodes.NOTIFICATION_IMAGE_URL);
    }
}
