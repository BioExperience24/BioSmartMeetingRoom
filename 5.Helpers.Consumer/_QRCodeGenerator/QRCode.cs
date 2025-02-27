using QRCoder;
using SkiaSharp;

namespace _5.Helpers.Consumer._QRCodeGenerator
{
    public class QRCode : AbstractQRCode, IDisposable
    {
        public QRCode()
        {
        }

        public QRCode(QRCodeData data)
            : base(data)
        {
        }

        public SKBitmap GetGraphic(int pixelsPerModule)
        {
            return GetGraphic(pixelsPerModule, SKColors.Black, SKColors.White, drawQuietZones: true);
        }

        public SKBitmap GetGraphic(int pixelsPerModule, string darkColorHtmlHex, string lightColorHtmlHex, bool drawQuietZones = true)
        {
            return GetGraphic(pixelsPerModule, SKColor.Parse(darkColorHtmlHex), SKColor.Parse(lightColorHtmlHex), drawQuietZones);
        }

        public SKBitmap GetGraphic(int pixelsPerModule, SKColor darkColor, SKColor lightColor, bool drawQuietZones = true)
        {
            int num = (base.QrCodeData.ModuleMatrix.Count - ((!drawQuietZones) ? 8 : 0)) * pixelsPerModule;
            int num2 = ((!drawQuietZones) ? (4 * pixelsPerModule) : 0);
            SKBitmap bitmap = new SKBitmap(num, num);
            using SKCanvas canvas = new SKCanvas(bitmap);
            using SKPaint darkPaint = new SKPaint { Color = darkColor };
            using SKPaint lightPaint = new SKPaint { Color = lightColor };
            canvas.Clear(lightColor);
            for (int i = 0; i < num + num2; i += pixelsPerModule)
            {
                for (int j = 0; j < num + num2; j += pixelsPerModule)
                {
                    if (base.QrCodeData.ModuleMatrix[(j + pixelsPerModule) / pixelsPerModule - 1][(i + pixelsPerModule) / pixelsPerModule - 1])
                    {
                        canvas.DrawRect(new SKRect(i - num2, j - num2, i - num2 + pixelsPerModule, j - num2 + pixelsPerModule), darkPaint);
                    }
                    else
                    {
                        canvas.DrawRect(new SKRect(i - num2, j - num2, i - num2 + pixelsPerModule, j - num2 + pixelsPerModule), lightPaint);
                    }
                }
            }

            return bitmap;
        }
    }
}