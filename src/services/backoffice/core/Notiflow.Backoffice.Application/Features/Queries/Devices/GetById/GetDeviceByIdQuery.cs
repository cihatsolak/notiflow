namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed record GetDeviceByIdQuery : IRequest<Result<GetDeviceByIdQueryResult>>
{
    public int Id { get; init; }

    public GetDeviceByIdQuery(int id)
    {
        Id = id;
    }
}
