namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Contains extension methods for JSON serialization and deserialization.
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Converts the specified object to a JSON string.
    /// </summary>
    /// <param name="value">The object to be serialized.</param>
    /// <returns>A JSON string representation of the object.</returns>
    public static string ToJsonString(this object value)
    {
        if (value is null)
            return default;

        return JsonSerializer.Serialize(value);
    }

    /// <summary>
    /// Deserializes the specified JSON string to an instance of the specified type.
    /// </summary>
    /// <typeparam name="TModel">The type to which the JSON string should be deserialized.</typeparam>
    /// <param name="value">The JSON string to be deserialized.</param>
    /// <returns>An instance of the specified type deserialized from the JSON string.</returns>
    public static TModel AsModel<TModel>(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;

        return JsonSerializer.Deserialize<TModel>(value, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
