namespace Notiflow.Backoffice.Application.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class DeviceException : Exception
{
    public DeviceException()
    {
    }

    public DeviceException(string message) : base(message)
    {
    }

    public DeviceException(string message, Exception innerException) : base(message, innerException)
    {
    }
}