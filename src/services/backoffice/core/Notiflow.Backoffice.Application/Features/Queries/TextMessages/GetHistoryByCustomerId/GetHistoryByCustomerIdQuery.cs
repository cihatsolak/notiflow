namespace Notiflow.Backoffice.Application.Features.Queries.TextMessages.GetHistoryByCustomerId;

public sealed record GetHistoryByCustomerIdQuery : IRequest<Response<IEnumerable<GetHistoryByCustomerIdQueryResponse>>>
{
    public int CustomerId { get; init; }
}



public sealed record GetHistoryByCustomerIdQueryResponse
{

}

public sealed class GetHistoryByCustomerIdQueryHandler : IRequestHandler<GetHistoryByCustomerIdQuery, Response<IEnumerable<GetHistoryByCustomerIdQueryResponse>>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<GetHistoryByCustomerIdQueryHandler> _logger;

    public GetHistoryByCustomerIdQueryHandler(INotiflowUnitOfWork uow, ILogger<GetHistoryByCustomerIdQueryHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<IEnumerable<GetHistoryByCustomerIdQueryResponse>>> Handle(GetHistoryByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        var asd = _uow.TenantRead
        throw new NotImplementedException();
    }
}