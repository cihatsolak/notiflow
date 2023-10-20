namespace Notiflow.Backoffice.Application.Features.Commands.Devices.DataTable;

public sealed record DeviceDataTableCommand : DtParameters, IRequest<Result<DtResult<DeviceDataTableResult>>>;