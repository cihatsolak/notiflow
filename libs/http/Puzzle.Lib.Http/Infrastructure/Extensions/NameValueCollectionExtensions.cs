namespace Puzzle.Lib.Http.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for HttpClient objects.
/// </summary>
public static class NameValueCollectionExtensions
{
    /// <summary>
    /// Add authentication and authorization token to http request
    /// </summary>
    /// <param name="token">authentication and authorization token</param>
    /// <returns>type of name value collection</returns>
    /// <exception cref="ArgumentNullException">thrown when token value is empty or null</exception>
    public static NameValueCollection CreateCollectionForBearerToken(string token)
    {
        ArgumentException.ThrowIfNullOrEmpty(token);

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
    /// <exception cref="ArgumentNullException">thrown when name value collection is null or token empty|null</exception>
    public static NameValueCollection AddBearerToken(this NameValueCollection nameValueCollection, string token)
    {
        ArgumentNullException.ThrowIfNull(nameValueCollection);
        ArgumentException.ThrowIfNullOrEmpty(token);

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
    public static NameValueCollection Generate(string name, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(value);

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
    public static NameValueCollection AddItem(this NameValueCollection nameValueCollection, string name, string value)
    {
        ArgumentNullException.ThrowIfNull(nameValueCollection);
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(value);

        nameValueCollection.Add(name, value);

        return nameValueCollection;
    }
}