namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Contains extension methods for working with enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the <see cref="DescriptionAttribute"/> value of an <see cref="Enum"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Enum"/> value to get the description of.</param>
    /// <returns>The description of the <see cref="Enum"/> value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="value"/> is null.</exception>
    public static string GetEnumDescription(this Enum value)
    {
        ArgumentNullException.ThrowIfNull(value);

        FieldInfo fi = value.GetType().GetField(value.ToString());

        if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Length != 0)
        {
            return attributes[0].Description;
        }

        return value.ToString();
    }

    /// <summary>
    /// Converts an integer value to its equivalent <see cref="Enum"/> value.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <see cref="Enum"/> to convert to.</typeparam>
    /// <param name="value">The integer value to convert to an <see cref="Enum"/>.</param>
    /// <returns>The equivalent <see cref="Enum"/> value of the integer.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the <paramref name="value"/> is less than -1.</exception>
    public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
    {
        if (Math.Sign(value) == -1)
            throw new ArgumentOutOfRangeException(nameof(value));

        return (TEnum)Enum.ToObject(typeof(TEnum), value);
    }

    /// <summary>
    /// Converts a string description to its equivalent <see cref="Enum"/> value.
    /// </summary>
    /// <typeparam name="TEnum">The type of the <see cref="Enum"/> to convert to.</typeparam>
    /// <param name="description">The string description to convert to an <see cref="Enum"/>.</param>
    /// <returns>The equivalent <see cref="Enum"/> value of the string description.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="description"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="description"/> is empty or consists of only whitespace characters.</exception>
    public static TEnum ToEnumByDescription<TEnum>(this string description) where TEnum : Enum
    {
        ArgumentException.ThrowIfNullOrEmpty(description);

        foreach (var field in typeof(TEnum).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description)
                    return (TEnum)field.GetValue(null);
            }
            else
            {
                if (field.Name == description)
                    return (TEnum)field.GetValue(null);
            }
        }

        throw new ArgumentNullException(nameof(description));
    }
}
