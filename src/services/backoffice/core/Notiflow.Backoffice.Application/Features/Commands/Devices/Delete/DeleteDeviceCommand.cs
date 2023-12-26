namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;

public sealed record DeleteDeviceCommand : IRequest<Result<Unit>>
{
    public int Id { get; init; }

    public DeleteDeviceCommand(int id)
    {
        Id = id;
    }
}
