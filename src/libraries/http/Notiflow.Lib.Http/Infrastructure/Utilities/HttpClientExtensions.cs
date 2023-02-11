namespace Notiflow.Lib.Http.Infrastructure.Utilities
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Add authentication and authorization token to http request
        /// </summary>
        /// <param name="token">authentication and authorization token</param>
        /// <returns>type of name value collection</returns>
        /// <exception cref="ArgumentNullException">thrown when token value is empty or null</exception>
        public static NameValueCollection SetBearerToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token), ExceptionMessage.TokenNotFound);

            return new NameValueCollection()
            {
                {  HeaderNames.Authorization, $"Bearer {token}"}
            };
        }

        /// <summary>
        /// Create name value collection to add to http request
        /// </summary>
        /// <param name="name">collection item name</param>
        /// <param name="value">collection item value</param>
        /// <returns>type of name value collection</returns>
        /// <exception cref="ArgumentNullException">thrown when name is empty|null or value empty|null</exception>
        public static NameValueCollection GenerateHeader(string name, string value)
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

        /// <summary>
        /// Add new item to existing name value collection
        /// </summary>
        /// <param name="nameValueCollection">current name value collection</param>
        /// <param name="name">collection item name</param>
        /// <param name="value">collection item value</param>
        /// <returns>type of name value collection</returns>
        /// <exception cref="ArgumentNullException">thrown when name is empty|null or value empty|null</exception>
        public static void AddHeaderItem(this NameValueCollection nameValueCollection, string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), ExceptionMessage.NameNotFound);

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), ExceptionMessage.ValueNotFound);

            nameValueCollection.Add(name, value);
        }
    }
}