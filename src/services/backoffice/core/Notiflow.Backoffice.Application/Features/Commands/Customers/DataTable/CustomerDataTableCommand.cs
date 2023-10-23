namespace Notiflow.Backoffice.Application.Features.Commands.Customers.DataTable;

public sealed record CustomerDataTableCommand : DtParameters, IRequest<Result<DtResult<CustomerDataTableCommandResult>>>
{
}
