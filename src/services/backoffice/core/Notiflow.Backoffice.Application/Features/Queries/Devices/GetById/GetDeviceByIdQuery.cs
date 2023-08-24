namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed record GetDeviceByIdQuery : IRequest<Response<GetDeviceByIdQueryResponse>>
{
    public required int Id { get; init; }
}
