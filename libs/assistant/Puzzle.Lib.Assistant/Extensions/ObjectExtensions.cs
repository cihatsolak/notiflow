namespace Puzzle.Lib.Assistant.Extensions;

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

    /// <summary>
    /// Gets the generic type name of the specified type.
    /// </summary>
    /// <param name="type">The type to get the generic type name for.</param>
    /// <returns>The generic type name of the specified type.</returns>
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
