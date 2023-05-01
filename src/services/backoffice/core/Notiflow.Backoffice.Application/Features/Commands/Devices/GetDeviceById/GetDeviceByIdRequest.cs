namespace Notiflow.Backoffice.Application.Features.Commands.Devices.GetDeviceById;

public sealed record GetDeviceByIdRequest : IRequest<ResponseData<GetDeviceByIdResponse>>
{
    public int Id { get; init; }
}
