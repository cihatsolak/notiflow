namespace Notiflow.Backoffice.Application.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public sealed class DeviceException : Exception
{
    public DeviceException()
    {
    }

    public DeviceException(string message) : base(message)
    {
    }

    private DeviceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DeviceException(string message, Exception innerException) : base(message, innerException)
    {
    }
}