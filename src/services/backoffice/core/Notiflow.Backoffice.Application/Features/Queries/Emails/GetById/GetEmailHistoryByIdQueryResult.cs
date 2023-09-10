namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed record GetEmailHistoryByIdQueryResult
{
    public int Id { get; init; }
    public string Recipients { get; init; }
    public string Cc { get; init; }
    public string Bcc { get; init; }
    public string Subject { get; init; }
    public string Body { get; init; }
    public bool IsSent { get; init; }
    public bool IsBodyHtml { get; init; }
    public string ErrorMessage { get; init; }
    public DateTime SentDate { get; init; }
}
