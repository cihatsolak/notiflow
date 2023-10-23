namespace Notiflow.Backoffice.Application.Features.Commands.Emails.Send;

public sealed record SendEmailCommand : IRequest<Result<Unit>>
{
    public required string Body { get; init; }
    public required string Subject { get; init; }
    public required List<int> CustomerIds { get; init; }
    public required List<string> CcAddresses { get; init; }
    public required List<string> BccAddresses { get; init; }
    public required bool IsBodyHtml { get; init; }
}
