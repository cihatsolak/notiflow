namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed record GetDeviceByIdQuery : IRequest<Result<GetDeviceByIdQueryResult>>
{
    public required int Id { get; init; }
}
