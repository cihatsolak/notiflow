using Newtonsoft.Json;

namespace Notiflow.Backoffice.Application.Models.Huawei
{
    public class HuaweiNotificationModel
    {
        [JsonPropertyName("validate_only")]
        public bool ValidateOnly { get; set; }

        [JsonPropertyName("message")]
        public HuaweiPushMessageModel HuaweiPushMessage { get; set; }
    }

    public class HuaweiPushMessageModel
    {
        /// <summary>
        /// Bildirim ile gönderilecek parametreler
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }

        /// <summary>
        /// Cihaz token'ı
        /// </summary>
        [JsonProperty("token")]
        public List<string> DeviceTokens { get; set; }
    }
}
