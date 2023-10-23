namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;

public sealed record DeleteDeviceCommand : IRequest<Result<Unit>>
{
    public required int Id { get; init; }
}
