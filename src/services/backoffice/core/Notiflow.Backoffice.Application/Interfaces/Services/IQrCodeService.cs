namespace Notiflow.Backoffice.Application.Interfaces.Services;

/// <summary>
/// Provides a service for generating QR codes from the given text.
/// </summary>
public interface IQrCodeService
{
    /// <summary>
    /// Generates a QR code image from the specified text.
    /// </summary>
    /// <param name="text">The text to be encoded in the QR code.</param>
    /// <returns>The byte array representing the QR code image.</returns>
    byte[] Generate(string text);
}