namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record UpdateCustomerBlockingCommand(int Id, bool IsBlocked) : IRequest<Result<Unit>>;

public sealed class UpdateCustomerBlockingCommandHandler : IRequestHandler<UpdateCustomerBlockingCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateCustomerBlockingCommandHandler> _logger;

    public UpdateCustomerBlockingCommandHandler(
        INotiflowUnitOfWork uow,
        ILogger<UpdateCustomerBlockingCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateCustomerBlockingCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<Unit>.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        if (customer.IsBlocked == request.IsBlocked)
        {
            _logger.LogWarning("The current disability situation is no different from the situation to be changed. Customer ID: {customerId}", request.Id);
            return Result<Unit>.Status400BadRequest(ResultCodes.CUSTOMER_BLOCKING_STATUS_EXISTS);
        }

        customer.IsBlocked = request.IsBlocked;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Customer with id {id} is {status}.",
            request.Id,
            request.IsBlocked ? "blocked" : "unblocked");

        return Result<Unit>.Status204NoContent(ResultCodes.CUSTOMER_BLOCK_STATUS_UPDATED);
    }
}

public sealed class UpdateCustomerBlockingCommandValidator : AbstractValidator<UpdateCustomerBlockingCommand>
{
    public UpdateCustomerBlockingCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}
