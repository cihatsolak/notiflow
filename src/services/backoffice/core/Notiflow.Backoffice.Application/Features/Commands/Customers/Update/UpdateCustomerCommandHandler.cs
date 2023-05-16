namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Update;

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<EmptyResponse>>
{
    private readonly INotiflowUnitOfWork _uow;

    public UpdateCustomerCommandHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Response<EmptyResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
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
