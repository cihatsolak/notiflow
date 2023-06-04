namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateEmail;

public sealed class UpdateCustomerEmailCommandHandler : IRequestHandler<UpdateCustomerEmailCommand, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateCustomerEmailCommandHandler> _logger;

    public UpdateCustomerEmailCommandHandler(INotiflowUnitOfWork uow, ILogger<UpdateCustomerEmailCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(UpdateCustomerEmailCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            _logger.LogWarning("Customer not found. ID: {@id}", request.Id);
            return Response<Unit>.Fail(ErrorCodes.CUSTOMER_NOT_FOUND);
        }

        if (customer.Email == request.Email)
        {
            _logger.LogWarning("The e-mail address to be changed is the same as in the current one. Customer ID: {@id}", request.Id);
            return Response<Unit>.Fail(ErrorCodes.CUSTOMER_EMAIL_ADDRESS_SAME);
        }

        customer.Email = request.Email;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The customer's email address has been updated. Customer ID: {@id}", request.Id);

        return Response<Unit>.Success(Unit.Value);
    }
}
