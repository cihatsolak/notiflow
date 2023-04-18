namespace Notiflow.Backoffice.Application.Models
{
    public class FirebaseSingleRequest
    {
        [JsonPropertyName("to")]
        public string Token { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("notification")]
        public FirebaseMessage FirebaseMessage { get; set; }
    }

    public class FirebaseMultipleRequest
    {
        [JsonPropertyName("registration_ids")]
        public List<string> Tokens { get; set; }

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
        public string Text { get; set; }

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; }
    }
}
