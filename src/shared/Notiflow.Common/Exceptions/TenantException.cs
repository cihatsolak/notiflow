namespace Notiflow.Common.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class TenantException : Exception
{
    public TenantException()
    {
    }

    public TenantException(string message) : base(message)
    {
    }

    public TenantException(string message, Exception innerException) : base(message, innerException)
    {
    }
}