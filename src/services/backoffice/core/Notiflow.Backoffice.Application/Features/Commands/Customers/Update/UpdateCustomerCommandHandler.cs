namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Update;

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ValidationErrorCodes> _localizer;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;

    public UpdateCustomerCommandHandler(
        INotiflowUnitOfWork uow,
        ILocalizerService<ValidationErrorCodes> localizer,
        ILogger<UpdateCustomerCommandHandler> logger)
    {
        _uow = uow;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<Unit>.Failure(StatusCodes.Status404NotFound, _localizer[ValidationErrorCodes.CUSTOMER_NOT_FOUND]);
        }

        ObjectMapper.Mapper.Map(request, customer);

        _uow.CustomerWrite.Update(customer);
        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Customer updated. ID: {customerId}", request.Id);

        return Result<Unit>.Success(StatusCodes.Status204NoContent, _localizer[ValidationErrorCodes.CUSTOMER_UPDATED], Unit.Value);
    }
}
