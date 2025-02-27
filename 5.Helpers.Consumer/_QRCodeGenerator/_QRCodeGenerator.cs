using System;
using System.IO;
using QRCoder;
using SkiaSharp;

namespace _5.Helpers.Consumer._QRCodeGenerator
{
    public sealed class _QRCodeGenerator
    {
        public static string GenerateQRBase64(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    using (SKBitmap qrCodeImage = qrCode.GetGraphic(20))
                    {
                        using (SKImage image = SKImage.FromBitmap(qrCodeImage))
                        {
                            using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                            {
                                byte[] byteImage = data.ToArray();
                                var base64String = Convert.ToBase64String(byteImage);
                                var dataUri = "data:image/png;base64," + base64String;
                                return dataUri;
                            }
                        }
                    }
                }
            }
        }
    }
}