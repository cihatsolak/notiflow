namespace Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;

public sealed record UpdateDeviceTokenCommand : IRequest<Result<Unit>>
{
    public required int Id { get; init; }
    public required string Token { get; init; }
}
