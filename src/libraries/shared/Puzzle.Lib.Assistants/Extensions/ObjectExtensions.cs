namespace Puzzle.Lib.Assistants.Extensions
{
    /// <summary>
    /// Object extensions
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// check if class properties empty or null
        /// </summary>
        /// <typeparam name="TSetting">type of setting interface</typeparam>
        /// <param name="class">type of setting interface</param>
        /// <returns>type of boolean</returns>
        public static bool CheckIfClassPropertiesEmptyOrNull<TSetting>(this TSetting @class)
        {
            foreach (PropertyInfo pi in @class.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(@class);
                    if (string.IsNullOrWhiteSpace(value))
                        return true;
                }
                else if (pi.PropertyType == typeof(int))
                {
                    int value = (int)pi.GetValue(@class);
                    if (0 >= value)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Object to integer
        /// </summary>
        /// <param name="value">object value</param>
        /// <returns>type of integer</returns>
        public static int ToInt(this object value)
        {
            ArgumentNullException.ThrowIfNull(value);

            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Get type name
        /// </summary>
        /// <param name="value">object value</param>
        /// <returns>type name</returns>
        public static string GetTypeName(this object value)
        {
            return value.GetType().GetGenericTypeName();
        }

        /// <summary>
        /// Converts the member name and value of the class as a dictionary 
        /// </summary>
        /// <param name="value">object value</param>
        /// <returns>type of dictionary</returns>
        public static Dictionary<string, string> ToDictionary<TValue>(this TValue value) where TValue : class, new()
        {
            return value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => prop.GetValue(value, null).ToString());
        }

        private static string GetGenericTypeName(this Type type)
        {
            string typeName;

            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return typeName;
        }
    }
}
