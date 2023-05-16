namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetDeviceById;

public sealed record GetDeviceByIdRequest : IRequest<Response<GetDeviceByIdResponse>>
{
    public int Id { get; init; }
}
