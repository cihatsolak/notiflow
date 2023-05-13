namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Update;

public sealed class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, Response<EmptyResponse>>
{
    private readonly INotiflowUnitOfWork _uow;

    public UpdateCustomerRequestHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Response<EmptyResponse>> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Response<EmptyResponse>.Fail(-1);
        }

        ObjectMapper.Mapper.Map(request, customer);

        _uow.CustomerWrite.Update(customer);
        await _uow.SaveChangesAsync(cancellationToken);

        return Response<EmptyResponse>.Success(1);
    }
}
