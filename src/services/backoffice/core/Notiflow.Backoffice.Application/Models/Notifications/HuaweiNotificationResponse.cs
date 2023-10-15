namespace Notiflow.Backoffice.Application.Models.Notifications
{
    public sealed record HuaweiNotificationResponse
    {
        private const int SUCCESS_CODE = 80000000;
        private const string SUCCESS_MESSAGE = "Success";


        [JsonPropertyName("code")]
        public string Code { get; init; }

        [JsonPropertyName("msg")]
        public string ErrorMessage { get; init; }

        [JsonPropertyName("requestId")]
        public string RequestId { get; init; }

        public bool Succeeded => Code.Equals(SUCCESS_CODE) && ErrorMessage.Equals(SUCCESS_MESSAGE, StringComparison.OrdinalIgnoreCase);
    }
}
