namespace Puzzle.Lib.Auth.Infrastructure.Exceptions;

[Serializable]
public sealed class ClaimException : Exception
{
    public ClaimException()
    {
    }

    public ClaimException(string parameterName) : base($"{parameterName} claim type not found.")
    {
    }

    private ClaimException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ClaimException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
