namespace Puzzle.Lib.Assistants.Extensions
{
    /// <summary>
    /// Enum extensions
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Provides the annotation property of the enum
        /// </summary>
        /// <param name="value">enumeration type</param>
        /// <returns>type of string</returns>
        /// <exception cref="ArgumentNullException">if method parameter null</exception>
        public static string GetEnumDescription(this Enum value)
        {
            ArgumentNullException.ThrowIfNull(value);

            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        /// <summary>
        /// Converts enumeration to integer value
        /// </summary>
        /// <param name="value">enumeration type</param>
        /// <returns>type of integer</returns>
        /// <exception cref="ArgumentNullException">if method parameter null</exception> 
        public static int ToInt(this Enum value)
        {
            ArgumentNullException.ThrowIfNull(value);

            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts integer value to integer value
        /// </summary>
        /// <typeparam name="TEnum">type of enum</typeparam>
        /// <param name="value">integer value</param>
        /// <returns>type of enum</returns>
        /// <exception cref="ArgumentOutOfRangeException">method parameter is not in the specified range</exception>
        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
        {
            if (-1 >= value)
                throw new ArgumentOutOfRangeException(nameof(value));

            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        /// <summary>
        /// Matches enum by description value
        /// </summary>
        /// <typeparam name="TEnum">type of enum</typeparam>
        /// <param name="description">Description for enum</param>
        /// <returns>type of enum</returns>
        /// <exception cref="ArgumentException">if the description is empty or null</exception>
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
}
