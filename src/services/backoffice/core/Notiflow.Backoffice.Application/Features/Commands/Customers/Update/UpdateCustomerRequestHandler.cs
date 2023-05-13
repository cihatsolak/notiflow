namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Update;

public sealed class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, Response>
{
    private readonly INotiflowUnitOfWork _uow;

    public UpdateCustomerRequestHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Response> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (true)
        {

        }
    }
}
