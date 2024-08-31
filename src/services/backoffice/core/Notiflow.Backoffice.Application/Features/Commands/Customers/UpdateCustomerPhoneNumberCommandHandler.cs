namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record UpdateCustomerPhoneNumberCommand(int Id, string PhoneNumber) : IRequest<Result>;

public sealed class UpdateCustomerPhoneNumberCommandHandler(
    INotiflowUnitOfWork uow,
    ILogger<UpdateCustomerPhoneNumberCommandHandler> logger) : IRequestHandler<UpdateCustomerPhoneNumberCommand, Result>
{
    public async Task<Result> Handle(UpdateCustomerPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var customer = await uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        if (string.Equals(customer.PhoneNumber, request.PhoneNumber, StringComparison.OrdinalIgnoreCase))
        {
            logger.LogWarning("The phone number to be changed is the same as in the current one. Customer ID: {customerId}", request.Id);
            return Result.Status400BadRequest(ResultCodes.CUSTOMER_PHONE_NUMBER_SAME);
        }

        customer.PhoneNumber = request.PhoneNumber;

        await uow.SaveChangesAsync(cancellationToken);

        logger.LogInformation("The customer's phone number has been updated. ID: {customerId}", request.Id);

        return Result.Status204NoContent(ResultCodes.CUSTOMER_PHONE_NUMBER_UPDATED);
    }
}

public sealed class UpdateCustomerPhoneNumberCommandValidator : AbstractValidator<UpdateCustomerPhoneNumberCommand>
{
    public UpdateCustomerPhoneNumberCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
        RuleFor(p => p.PhoneNumber).MobilePhone(localizer[ValidationErrorMessage.PHONE_NUMBER]);
    }
}