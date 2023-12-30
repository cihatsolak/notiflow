namespace Puzzle.Lib.Auth.Infrastructure;

[ExcludeFromCodeCoverage]
public sealed class JwtClaimException : Exception
{
    public JwtClaimException()
    {
    }

    public JwtClaimException(string parameterName) : base($"{parameterName} claim type not found.")
    {
    }

    public JwtClaimException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
