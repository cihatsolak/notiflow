namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetDeviceById;

public sealed record GetDeviceByIdQuery : IRequest<Response<GetDeviceByIdQueryResponse>>
{
    public required int Id { get; init; }
}
