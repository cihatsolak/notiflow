namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ValidationErrorCodes> _localizer;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;

    public DeleteCustomerCommandHandler(
        INotiflowUnitOfWork uow,
         ILocalizerService<ValidationErrorCodes> localizer,
        ILogger<DeleteCustomerCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task<Result<Unit>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await _uow.CustomerWrite.ExecuteDeleteAsync(request.Id, cancellationToken);
        if (!isDeleted)
        {
            return Result<Unit>.Failure(StatusCodes.Status404NotFound, _localizer[ValidationErrorCodes.CUSTOMER_NOT_DELETED]);
        }

        _logger.LogInformation("Customer deleted. ID: {customerId}", request.Id);

        return Result<Unit>.Success(StatusCodes.Status204NoContent, _localizer[ValidationErrorCodes.CUSTOMER_DELETED], Unit.Value);
    }
}
