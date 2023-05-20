namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Update;

public sealed record UpdateDeviceCommand : IRequest<Response<Unit>>
{
    public required int Id { get; init; }
    public required OSVersion OSVersion { get; init; }
    public required string Code { get; init; }
    public required string Token { get; init; }
    public required CloudMessagePlatform CloudMessagePlatform { get; init; }
}
