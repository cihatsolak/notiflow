namespace Puzzle.Lib.Assistants.Extensions
{
    /// <summary>
    /// Provides extension methods for objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks if the properties of the given class are empty or null.
        /// </summary>
        /// <typeparam name="TClass">The type of the class to check.</typeparam>
        /// <param name="class">The class instance to check.</param>
        /// <returns>Returns true if any of the properties are empty or null, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the class instance is null.</exception>
        public static bool CheckIfClassPropertiesEmptyOrNull<TClass>(this TClass @class) where TClass : class, new()
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
        /// Converts the given object to an integer.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>Returns the integer value of the object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the object is null.</exception>
        public static int ToInt(this object value)
        {
            ArgumentNullException.ThrowIfNull(value);

            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Gets the type name of the given object.
        /// </summary>
        /// <param name="value">The object to get the type name for.</param>
        /// <returns>Returns the type name of the object.</returns>
        public static string GetTypeName(this object value)
        {
            return value.GetType().GetGenericTypeName();
        }

        /// <summary>
        /// Converts the given object to a dictionary of property names and values.
        /// </summary>
        /// <typeparam name="TClass">The type of the object to convert.</typeparam>
        /// <param name="value">The object to convert.</param>
        /// <returns>Returns a dictionary of property names and values.</returns>
        public static Dictionary<string, string> ToDictionary<TClass>(this TClass value) where TClass : class, new()
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
