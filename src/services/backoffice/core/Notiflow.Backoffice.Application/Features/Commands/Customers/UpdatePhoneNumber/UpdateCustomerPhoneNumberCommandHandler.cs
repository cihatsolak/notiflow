﻿namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdatePhoneNumber;

public sealed class UpdateCustomerPhoneNumberCommandHandler : IRequestHandler<UpdateCustomerPhoneNumberCommand, Response<EmptyResponse>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateCustomerPhoneNumberCommandHandler> _logger;

    public UpdateCustomerPhoneNumberCommandHandler(INotiflowUnitOfWork uow, ILogger<UpdateCustomerPhoneNumberCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<EmptyResponse>> Handle(UpdateCustomerPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            _logger.LogWarning("Customer not found. ID: {@id}", request.Id);
            return Response<EmptyResponse>.Fail(ErrorCodes.CUSTOMER_NOT_FOUND);
        }

        if (customer.PhoneNumber == request.PhoneNumber)
        {
            _logger.LogWarning("The phone number to be changed is the same as in the current one.. Customer ID: {@id}", request.Id);
            return Response<EmptyResponse>.Fail(ErrorCodes.CUSTOMER_NOT_FOUND);
        }

        customer.PhoneNumber = request.PhoneNumber;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The customer's phone number has been updated. Customer ID: {@id}", request.Id);

        return Response<EmptyResponse>.Success(-1);
    }
}