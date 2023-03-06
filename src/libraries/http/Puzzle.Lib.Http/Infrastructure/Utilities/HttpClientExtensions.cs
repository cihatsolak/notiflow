namespace Puzzle.Lib.Http.Infrastructure.Utilities
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Add authentication and authorization token to http request
        /// </summary>
        /// <param name="token">authentication and authorization token</param>
        /// <returns>type of name value collection</returns>
        /// <exception cref="ArgumentNullException">thrown when token value is empty or null</exception>
        public static NameValueCollection CreateCollectionForBearerToken(string token)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(token));

            return new NameValueCollection()
            {
                {  HeaderNames.Authorization, $"Bearer {token}"}
            };
        }

        /// <summary>
        /// Add authentication and authorization token to http request
        /// </summary>
        /// <param name="nameValueCollection">current name value collection</param>
        /// <param name="token">authentication and authorization token</param>
        /// <returns>type of name value collection</returns>
        /// <exception cref="ArgumentNullException">thrown when token value is empty or null</exception>
        public static NameValueCollection AddBearerTokenToHeader(this NameValueCollection nameValueCollection, string token)
        {
            ArgumentNullException.ThrowIfNull(nameValueCollection);
            ArgumentException.ThrowIfNullOrEmpty(nameof(token));

            nameValueCollection.Add(HeaderNames.Authorization, $"Bearer {token}");
            return nameValueCollection;
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
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(value));

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
        /// <exception cref="ArgumentNullException">thrown when name is empty|null or value empty|null or name value collection null</exception>
        public static void AddHeaderItem(this NameValueCollection nameValueCollection, string name, string value)
        {
            ArgumentNullException.ThrowIfNull(nameValueCollection);
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(nameof(value));

            nameValueCollection.Add(name, value);
        }
    }
}