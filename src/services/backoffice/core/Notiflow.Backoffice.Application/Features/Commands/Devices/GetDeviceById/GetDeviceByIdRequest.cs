namespace Notiflow.Backoffice.Application.Features.Commands.Devices.GetDeviceById;

public sealed record GetDeviceByIdRequest : IRequest<ResponseModel<GetDeviceByIdResponse>>
{
    public int Id { get; init; }
}
