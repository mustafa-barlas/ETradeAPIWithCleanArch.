using ETradeAPI.Application.Abstractions.Services;
using QRCoder;

namespace ETradeAPI.Infrastructure.Services;

public class QRCodeService : IQRCodeService
{

    public byte[] GenerateQRCode(string text)
    {
        QRCodeGenerator codeGenerator = new();
        QRCodeData data = codeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new(data);
        byte[] byteGraphic = qrCode.GetGraphic(10, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 });

        return byteGraphic;
    }
}