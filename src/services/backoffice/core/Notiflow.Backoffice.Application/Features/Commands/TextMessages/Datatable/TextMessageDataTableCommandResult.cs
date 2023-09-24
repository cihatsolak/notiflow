namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Datatable;

public sealed record TextMessageDataTableCommandResult
{
    public required int Id { get; init; }
    public required string Message { get; init; }
    public required bool IsSent { get; init; }
    public required DateTime SentDate { get; init; }
}