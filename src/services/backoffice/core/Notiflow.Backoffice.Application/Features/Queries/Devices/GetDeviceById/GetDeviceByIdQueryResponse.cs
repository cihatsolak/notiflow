namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetDeviceById;

public sealed record GetDeviceByIdQueryResponse
{
    public OSVersion OSVersion { get; init; }
    public string Code { get; init; }
    public string Token { get; init; }
    public CloudMessagePlatform CloudMessagePlatform { get; init; }
}
