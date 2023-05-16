namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendMultiple;

public sealed record SendMultipleTextMessageCommand : IRequest<Response<Unit>>
{
    public required List<int> CustomerIds { get; init; }
    public required string Message { get; init; }
}
