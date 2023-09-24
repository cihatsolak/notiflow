namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class QrCodeManager : IQrCodeService
{
    public byte[] Generate(string text)
    {
        using QRCodeGenerator qRCodeGenerator = new();
        QRCodeData data = qRCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        return new PngByteQRCode(data).GetGraphic(10, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 });
    }
}
