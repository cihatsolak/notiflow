namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Update;

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;

    public UpdateCustomerCommandHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Response<Unit>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Response<Unit>.Fail(-1);
        }

        ObjectMapper.Mapper.Map(request, customer);

        _uow.CustomerWrite.Update(customer);
        await _uow.SaveChangesAsync(cancellationToken);

        return Response<Unit>.Success(1);
    }
}
