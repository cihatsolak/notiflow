namespace Notiflow.Backoffice.Application.Models
{
    public class FirebaseSingleNotificationRequest
    {
        [JsonPropertyName("to")]
        public string DeviceToken { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("notification")]
        public FirebaseMessage FirebaseMessage { get; set; }
    }

    public class FirebaseMultipleNotificationRequest
    {
        [JsonPropertyName("registration_ids")]
        public List<string> DeviceTokens { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("notification")]
        public FirebaseMessage FirebaseMessage { get; set; }
    }

    public class FirebaseMessage
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Message { get; set; }

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; }
    }
}
