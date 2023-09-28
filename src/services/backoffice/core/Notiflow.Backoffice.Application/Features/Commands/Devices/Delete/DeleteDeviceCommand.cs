namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;

public sealed record DeleteDeviceCommand : IRequest<ApiResponse<Unit>>
{
    public required int Id { get; init; }
}
