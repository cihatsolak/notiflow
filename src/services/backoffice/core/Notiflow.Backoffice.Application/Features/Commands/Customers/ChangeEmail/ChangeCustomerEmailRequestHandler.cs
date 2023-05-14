namespace Notiflow.Backoffice.Application.Features.Commands.Customers.ChangeEmail;

public sealed class ChangePhoneNumberRequestHandler : IRequestHandler<ChangeCustomerEmailRequest, Response<EmptyResponse>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<ChangePhoneNumberRequestHandler> _logger;

    public ChangePhoneNumberRequestHandler(INotiflowUnitOfWork uow, ILogger<ChangePhoneNumberRequestHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<EmptyResponse>> Handle(ChangeCustomerEmailRequest request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            _logger.LogWarning("Customer not found. ID: {@id}", request.Id);
            return Response<EmptyResponse>.Fail(ErrorCodes.CUSTOMER_NOT_FOUND);
        }

        if (customer.Email == request.Email)
        {
            _logger.LogWarning("The e-mail address to be changed is the same as in the current one. Customer ID: {@id}", request.Id);
            return Response<EmptyResponse>.Fail(ErrorCodes.CUSTOMER_NOT_FOUND);
        }

        customer.Email = request.Email;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The customer's email address has been updated. Customer ID: {@id}", request.Id);

        return Response<EmptyResponse>.Success(-1);
    }
}
