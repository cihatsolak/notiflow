namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendSingle;

public sealed record SendSingleTextMessageRequest : IRequest<Response<Unit>>
{
    public required int CustomerId { get; init; }
    public required string Message { get; init; }
}
