namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Insert;

public sealed record InsertDeviceRequest : IRequest<ResponseModel<Unit>>
{
    public required int CustomerId { get; init; }
    public required OSVersion OSVersion { get; init; }
    public required string Code { get; init; }
    public required string Token { get; init; }
    public required CloudMessagePlatform CloudMessagePlatform { get; init; }
}