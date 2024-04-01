namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record UpdateCustomerBlockingCommand(int Id, bool IsBlocked) : IRequest<Result<EmptyResponse>>;

public sealed class UpdateCustomerBlockingCommandHandler(
    INotiflowUnitOfWork uow,
    ILogger<UpdateCustomerBlockingCommandHandler> logger) : IRequestHandler<UpdateCustomerBlockingCommand, Result<EmptyResponse>>
{
    public async Task<Result<EmptyResponse>> Handle(UpdateCustomerBlockingCommand request, CancellationToken cancellationToken)
    {
        var customer = await uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<EmptyResponse>.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        if (customer.IsBlocked == request.IsBlocked)
        {
            logger.LogWarning("The current disability situation is no different from the situation to be changed. Customer ID: {customerId}", request.Id);
            return Result<EmptyResponse>.Status400BadRequest(ResultCodes.CUSTOMER_BLOCKING_STATUS_EXISTS);
        }

        customer.IsBlocked = request.IsBlocked;

        await uow.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Customer with id {id} is {status}.",
            request.Id,
            request.IsBlocked ? "blocked" : "unblocked");

        return Result<EmptyResponse>.Status204NoContent(ResultCodes.CUSTOMER_BLOCK_STATUS_UPDATED);
    }
}

public sealed class UpdateCustomerBlockingCommandValidator : AbstractValidator<UpdateCustomerBlockingCommand>
{
    public UpdateCustomerBlockingCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}
