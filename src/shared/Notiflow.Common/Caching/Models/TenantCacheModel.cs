﻿namespace Notiflow.Common.Caching.Models;

public sealed record TenantCacheModel
{
    public string Token { get; init; }
    public bool IsSendMessagePermission { get; init; }
    public bool IsSendNotificationPermission { get; init; }
    public bool IsSendEmailPermission { get; init; }
    public string FirebaseServerKey { get; init; }
    public string FirebaseSenderId { get; init; }
    public string HuaweiServerKey { get; init; }
    public string HuaweiSenderId { get; init; }
    public string MailFromAddress { get; init; }
    public string MailFromName { get; init; }
    public string MailReplyAddress { get; init; }
}