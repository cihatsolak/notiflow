namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Add;

public sealed class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Response<int>>
{
    private readonly INotiflowUnitOfWork _uow;

    public AddCustomerCommandHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Response<int>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isExists = await _uow.CustomerRead.IsExistsByPhoneNumberOrEmailAsync(request.PhoneNumber, request.Email, cancellationToken);
        if (isExists)
        {
            return Response<int>.Fail(-1);
        }

        var customer = ObjectMapper.Mapper.Map<Customer>(request);

        await _uow.CustomerWrite.InsertAsync(customer, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Response<int>.Success(-1, customer.Id);
    }
}
