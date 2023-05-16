namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendSingle;

public sealed record SendSingleTextMessageCommand : IRequest<Response<Unit>>
{
    public required int CustomerId { get; init; }
    public required string Message { get; init; }
}
