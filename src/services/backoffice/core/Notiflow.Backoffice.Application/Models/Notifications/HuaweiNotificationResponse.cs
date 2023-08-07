namespace Notiflow.Backoffice.Application.Models.Notifications
{
    public sealed record HuaweiNotificationResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; init; }

        [JsonPropertyName("msg")]
        public string ErrorMessage { get; init; }

        [JsonPropertyName("requestId")]
        public string RequestId { get; init; }

#pragma warning disable S3256 // "string.IsNullOrEmpty" should be used
        public bool Succeeded => !Code.Equals("") || !ErrorMessage.Equals("", StringComparison.OrdinalIgnoreCase);
#pragma warning restore S3256 // "string.IsNullOrEmpty" should be used
    }
}
