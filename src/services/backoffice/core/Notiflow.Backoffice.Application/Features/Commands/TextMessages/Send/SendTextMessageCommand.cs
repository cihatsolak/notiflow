﻿namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Send;

public sealed record SendTextMessageCommand : IRequest<Result<Unit>>
{
    public required List<int> CustomerIds { get; init; }
    public required string Message { get; init; }
}
