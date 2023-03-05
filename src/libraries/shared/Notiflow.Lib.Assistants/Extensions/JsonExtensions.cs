namespace Notiflow.Lib.Assistants.Extensions
{
    /// <summary>
    /// JSON Extensions
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts object type to string type
        /// </summary>
        /// <param name="value">type of object</param>
        /// <returns>type of string</returns>
        public static string ToJsonString(this object value)
        {
            if (value is null)
                return default;

            return JsonSerializer.Serialize(value);
        }

        /// <summary>
        /// Converts string type to specified type
        /// </summary>
        /// <typeparam name="TModel">deserialize type</typeparam>
        /// <param name="value">value to deserialize</param>
        /// <returns>type of TModel</returns>
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
}
