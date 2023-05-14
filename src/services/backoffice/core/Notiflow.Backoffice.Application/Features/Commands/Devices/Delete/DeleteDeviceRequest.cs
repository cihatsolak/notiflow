namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;

public sealed record DeleteDeviceRequest : IRequest<Response<Unit>>
{
    public int Id { get; init; }
}
