namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed record GetDeviceByIdQuery : IRequest<Response<GetDeviceByIdQueryResult>>
{
    public required int Id { get; init; }
}
