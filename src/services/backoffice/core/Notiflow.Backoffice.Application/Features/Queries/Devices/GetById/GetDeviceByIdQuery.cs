namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed record GetDeviceByIdQuery : IRequest<ApiResponse<GetDeviceByIdQueryResult>>
{
    public required int Id { get; init; }
}
