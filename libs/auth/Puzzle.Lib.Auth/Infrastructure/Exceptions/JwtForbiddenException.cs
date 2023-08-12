namespace Puzzle.Lib.Auth.Infrastructure.Exceptions;

[Serializable]
public sealed class JwtForbiddenException : Exception
{
    public JwtForbiddenException()
    {
    }

    public JwtForbiddenException(string message) : base(message)
    {
    }

    private JwtForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public JwtForbiddenException(string message, Exception innerException) : base(message, innerException)
    {
    }
}