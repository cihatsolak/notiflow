namespace Notiflow.IdentityServer.Service.Infrastructure.Exceptions;

[Serializable]
public sealed class TenantException : Exception
{
    public TenantException()
    {
    }

    public TenantException(string message) : base(message)
    {
    }

    private TenantException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TenantException(string message, Exception innerException) : base(message, innerException)
    {
    }
}