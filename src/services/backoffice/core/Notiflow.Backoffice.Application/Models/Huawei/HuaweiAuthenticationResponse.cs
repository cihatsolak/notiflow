namespace Notiflow.Backoffice.Application.Models.Huawei
{
    public sealed record HuaweiAuthenticationResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; init; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; init; }

        [JsonPropertyName("sub_error")]
        public int SubError { get; init; }

        [JsonPropertyName("error_description")]
        public string ErrorMessage { get; init; }

        [JsonPropertyName("error")]
        public int Error { get; init; }
    }
}
