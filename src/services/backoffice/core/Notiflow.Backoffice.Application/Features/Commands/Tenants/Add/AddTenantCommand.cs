namespace Notiflow.Backoffice.Application.Features.Commands.Tenants.Add;

public sealed record AddTenantCommand : IRequest<Response<int>>
{
    public required string Name { get; init; }
    public required string Definition { get; init; }
    public required Guid AppId { get; init; }
    public required string FirebaseServerKey { get; init; }
    public required string FirebaseSenderId { get; init; }
    public required string HuaweiServerKey { get; init; }
    public required string HuaweiSenderId { get; init; }
    public required string MailFromAddress { get; init; }
    public required string MailFromName { get; init; }
    public required string MailReplyAddress { get; init; }
    public required bool IsSendMessagePermission { get; init; }
    public required bool IsSendNotificationPermission { get; init; }
    public required bool IsSendEmailPermission { get; init; }
}
