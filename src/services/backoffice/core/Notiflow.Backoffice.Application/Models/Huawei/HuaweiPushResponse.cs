namespace Notiflow.Backoffice.Application.Models.Huawei
{
    public sealed record HuaweiPushResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; init; }

        [JsonPropertyName("msg")]
        public string ErrorMessage { get; init; }

        [JsonPropertyName("requestId")]
        public string RequestId { get; init; }
    }
}
