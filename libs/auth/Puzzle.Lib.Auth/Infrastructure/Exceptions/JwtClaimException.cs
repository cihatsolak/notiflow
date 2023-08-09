namespace Puzzle.Lib.Auth.Infrastructure.Exceptions;

[Serializable]
public sealed class JwtClaimException : Exception
{
    public JwtClaimException()
    {
    }

    public JwtClaimException(string parameterName) : base($"{parameterName} claim type not found.")
    {
    }

    private JwtClaimException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public JwtClaimException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
