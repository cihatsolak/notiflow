namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Datatable;

public sealed record TextMessageDataTableCommand : DtParameters, IRequest<Response<DtResult<TextMessageDataTableCommandResponse>>>
{
}

public sealed record TextMessageDataTableCommandResponse
{
    public required int Id { get; init; }
    public required string Message { get; init; }
    public required bool IsSent { get; init; }
    public required DateTime SentDate { get; init; }
}