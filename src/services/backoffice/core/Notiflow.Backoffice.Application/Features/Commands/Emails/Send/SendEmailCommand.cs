﻿namespace Notiflow.Backoffice.Application.Features.Commands.Emails.Send;

public sealed record SendEmailCommand : IRequest<Response<Unit>>
{
    public required List<int> CustomerIds { get; init; }
    public required List<string> CcAddresses { get; init; }
    public required List<string> BccAddresses { get; init; }
    public required string Subject { get; init; }
    public required string Body { get; init; }
}
