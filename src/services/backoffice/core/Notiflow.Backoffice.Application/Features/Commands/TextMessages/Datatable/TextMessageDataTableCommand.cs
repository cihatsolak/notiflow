namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Datatable;

public sealed record TextMessageDataTableCommand : DtParameters, IRequest<ApiResponse<DtResult<TextMessageDataTableCommandResult>>>
{
}
