namespace Notiflow.Backoffice.Application.Features.Commands.Devices.GetDeviceById;

public sealed record GetDeviceByIdResponse
{
    public OSVersion OSVersion { get; init; }
    public string Code { get; init; }
    public string Token { get; init; }
    public CloudMessagePlatform CloudMessagePlatform { get; init; }
}
