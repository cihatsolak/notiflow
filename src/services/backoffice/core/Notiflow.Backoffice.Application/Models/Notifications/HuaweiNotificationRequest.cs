﻿namespace Notiflow.Backoffice.Application.Models.Notifications
{
    public sealed record HuaweiNotificationRequest
    {
        [JsonPropertyName("validate_only")]
        public bool ValidateOnly { get; set; }

        [JsonPropertyName("message")]
        public HuaweiMessage Message { get; set; }
    }

    public sealed record HuaweiMessage
    {
        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("token")]
        public List<string> DeviceTokens { get; set; }
    }
}
