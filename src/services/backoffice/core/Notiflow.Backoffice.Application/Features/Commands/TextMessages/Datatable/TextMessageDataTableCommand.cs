namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Datatable;

public sealed record TextMessageDataTableCommand : DtParameters, IRequest<Response<DtResult<TextMessageDataTableCommandResult>>>
{
}
