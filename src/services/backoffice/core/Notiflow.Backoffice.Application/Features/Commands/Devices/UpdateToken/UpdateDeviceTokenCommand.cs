namespace Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;

public sealed record UpdateDeviceTokenCommand : IRequest<ApiResponse<Unit>>
{
    public required int Id { get; init; }
    public required string Token { get; init; }
}
