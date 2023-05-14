namespace Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;

public sealed record UpdateDeviceTokenRequest : IRequest<Response<Unit>>
{
    public int Id { get; init; }
    public string Token { get; init; }
}
