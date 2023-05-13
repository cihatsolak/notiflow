namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Add;

public sealed class AddCustomerRequestHandler : IRequestHandler<AddCustomerRequest, ResponseData<int>>
{
    private readonly INotiflowUnitOfWork _uow;

    public AddCustomerRequestHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ResponseData<int>> Handle(AddCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetCustomerByPhoneNumberOrEmailAsync(request.PhoneNumber, request.Email, cancellationToken);
        if (customer is not null)
        {
            return ResponseData<int>.Fail(-1);
        }

        customer = ObjectMapper.Mapper.Map<Customer>(request);

        await _uow.CustomerWrite.InsertAsync(customer, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return ResponseData<int>.Success(-1, customer.Id);
    }
}
