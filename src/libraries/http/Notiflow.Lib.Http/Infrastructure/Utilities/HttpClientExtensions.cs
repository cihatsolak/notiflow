namespace Notiflow.Lib.Http.Infrastructure.Utilities
{
    public static class HttpClientExtensions
    {
        public static NameValueCollection SetBearerToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token), ExceptionMessage.TokenNotFound);

            return new NameValueCollection()
            {
                {  HeaderNames.Authorization, $"Bearer {token}"}
            };
        }

        public static NameValueCollection SetHeader(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), ExceptionMessage.NameNotFound);

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), ExceptionMessage.ValueNotFound);

            return new NameValueCollection()
            {
                {  name, value}
            };
        }
    }
}
