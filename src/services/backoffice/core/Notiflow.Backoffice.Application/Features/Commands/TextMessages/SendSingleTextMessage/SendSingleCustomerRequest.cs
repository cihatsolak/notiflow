namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendSingleTextMessage;

public sealed record SendSingleCustomerRequest : IRequest<Response>
{
    public int CustomerId { get; init; }
    public string Text { get; init; }
}