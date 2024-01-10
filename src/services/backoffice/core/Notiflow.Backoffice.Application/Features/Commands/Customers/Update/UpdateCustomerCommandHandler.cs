namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Update;

public sealed record UpdateCustomerCommand(
    int Id,
    string Name,
    string Surname,
    string PhoneNumber,
    string Email,
    DateTime BirthDate,
    Gender Gender,
    MarriageStatus MarriageStatus
    )
    : IRequest<Result<Unit>>;

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultMessage> _localizer;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;

    public UpdateCustomerCommandHandler(
        INotiflowUnitOfWork uow,
        ILocalizerService<ResultMessage> localizer,
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
            return Result<Unit>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.CUSTOMER_NOT_FOUND]);
        }

        ObjectMapper.Mapper.Map(request, customer);

        _uow.CustomerWrite.Update(customer);
        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Customer updated. ID: {customerId}", request.Id);

        return Result<Unit>.Success(StatusCodes.Status204NoContent, _localizer[ResultMessage.CUSTOMER_UPDATED], Unit.Value);
    }
}
