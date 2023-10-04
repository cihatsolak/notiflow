namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateEmail;

public sealed class UpdateCustomerEmailCommandHandler : IRequestHandler<UpdateCustomerEmailCommand, ApiResponse<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateCustomerEmailCommandHandler> _logger;

    public UpdateCustomerEmailCommandHandler(INotiflowUnitOfWork uow, ILogger<UpdateCustomerEmailCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<ApiResponse<Unit>> Handle(UpdateCustomerEmailCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.CUSTOMER_NOT_FOUND);
        }

        if (string.Equals(customer.Email, request.Email, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("The e-mail address to be changed is the same as in the current one. Customer ID: {@id}", request.Id);
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.CUSTOMER_EMAIL_ADDRESS_SAME);
        }

        customer.Email = request.Email;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The customer's email address has been updated. ID: {@id}", request.Id);

        return ApiResponse<Unit>.Success(ResponseCodes.Success.CUSTOMER_EMAIL_UPDATED, Unit.Value);
    }
}
