namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendSingle;

public sealed record SendSingleTextMessageRequest : IRequest<Response<Unit>>
{
    public int CustomerId { get; init; }
    public string Message { get; init; }
}
