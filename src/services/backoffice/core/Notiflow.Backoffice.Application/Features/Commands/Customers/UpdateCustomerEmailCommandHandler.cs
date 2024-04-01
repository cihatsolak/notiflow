namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record UpdateCustomerEmailCommand(int Id, string Email) : IRequest<Result<EmptyResponse>>;

public sealed class UpdateCustomerEmailCommandHandler(
    INotiflowUnitOfWork uow,
    ILogger<UpdateCustomerEmailCommandHandler> logger) : IRequestHandler<UpdateCustomerEmailCommand, Result<EmptyResponse>>
{
    public async Task<Result<EmptyResponse>> Handle(UpdateCustomerEmailCommand request, CancellationToken cancellationToken)
    {
        var customer = await uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<EmptyResponse>.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        if (string.Equals(customer.Email, request.Email, StringComparison.OrdinalIgnoreCase))
        {
            logger.LogWarning("The e-mail address to be changed is the same as in the current one. Customer ID: {customerId}", request.Id);
            return Result<EmptyResponse>.Status400BadRequest(ResultCodes.CUSTOMER_EMAIL_ADDRESS_SAME);
        }

        customer.Email = request.Email;

        await uow.SaveChangesAsync(cancellationToken);

        logger.LogInformation("The customer's email address has been updated. ID: {customerId}", request.Id);

        return Result<EmptyResponse>.Status204NoContent(ResultCodes.CUSTOMER_EMAIL_UPDATED);
    }
}

public sealed class ChangePhoneNumberRequestValidator : AbstractValidator<UpdateCustomerEmailCommand>
{
    public ChangePhoneNumberRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
        RuleFor(p => p.Email).Email(localizer[ValidationErrorMessage.EMAIL]);
    }
}