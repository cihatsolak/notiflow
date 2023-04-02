namespace Puzzle.Lib.Security.Infrastructure.Exceptions
{
    /// <summary>
    /// Contains methods to check if an argument is a negative number.
    /// </summary>
    internal static class SecurityArgumentException
    {
        /// <summary>
        /// Throws an ArgumentException if the provided argument is a negative number.
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="paramName">The name of the parameter that the argument represents.</param>
        /// <exception cref="ArgumentException">Thrown when the argument is a negative number.</exception>
        internal static void ThrowIfNegativeNumber(int argument, string paramName = null)
        {
            if (Math.Sign(argument) == -1)
            {
                throw new ArgumentException("The value cannot be less than zero, negative number.", paramName);
            }
        }
    }
}
