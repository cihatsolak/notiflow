namespace Puzzle.Lib.Auth.Infrastructure.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when there is an authentication-related argument error.
    /// This class cannot be inherited.
    /// </summary>
    internal static class AuthArgumentException
    {
        /// <summary>
        /// Throws a <see cref="ClaimException"/> if the specified string argument is null, empty, or only contains whitespace.
        /// </summary>
        /// <param name="argument">The string argument to validate.</param>
        /// <param name="paramName">The name of the parameter being validated (automatically detected by the compiler).</param>
        internal static void ThrowIfNullOrEmpty([NotNull] string argument, [CallerArgumentExpression("argument")] string paramName = null)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ClaimException(paramName);
            }
        }

        /// <summary>
        /// Throws a <see cref="ClaimException"/> if the specified collection argument is null or empty.
        /// </summary>
        /// <param name="argument">The collection argument to validate.</param>
        /// <param name="paramName">The name of the parameter being validated (automatically detected by the compiler).</param>
        internal static void ThrowIfNullOrEmpty([NotNull] IEnumerable<string> argument, [CallerArgumentExpression("argument")] string paramName = null)
        {
            if (argument is null || !argument.Any())
            {
                throw new ClaimException(paramName);
            }
        }
    }

}
