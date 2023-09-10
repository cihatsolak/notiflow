namespace Notiflow.Backoffice.Application.Features.Commands.Devices.DataTable;

public sealed record DeviceDataTableCommand : DtParameters, IRequest<Response<DtResult<DeviceDataTableResult>>>
{
}