namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Add;

public sealed record AddDeviceCommand : IRequest<ApiResponse<int>>
{
    public required int CustomerId { get; init; }
    public required OSVersion OSVersion { get; init; }
    public required string Code { get; init; }
    public required string Token { get; init; }
    public required CloudMessagePlatform CloudMessagePlatform { get; init; }
}