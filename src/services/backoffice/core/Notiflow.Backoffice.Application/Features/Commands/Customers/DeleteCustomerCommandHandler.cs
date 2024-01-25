namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record DeleteCustomerCommand(int Id) : IRequest<Result<Unit>>;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;

    public DeleteCustomerCommandHandler(
        INotiflowUnitOfWork uow,
        ILogger<DeleteCustomerCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await _uow.CustomerWrite.ExecuteDeleteByIdAsync(request.Id, cancellationToken);
        if (!isDeleted)
        {
            return Result<Unit>.Failure(StatusCodes.Status404NotFound, ResultCodes.CUSTOMER_NOT_DELETED);
        }

        _logger.LogInformation("Customer deleted. ID: {customerId}", request.Id);

        return Result<Unit>.Success(StatusCodes.Status204NoContent, ResultCodes.CUSTOMER_DELETED, Unit.Value);
    }
}

public sealed class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}
