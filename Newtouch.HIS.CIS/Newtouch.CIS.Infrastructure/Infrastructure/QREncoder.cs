using System;
using System.Collections;
using Newtouch.CIS.Compat;
using QRCoder;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 二维码编码器 (ported from Framework CIS QREncoder).
    /// Returns PNG bytes instead of a System.Drawing.Bitmap: GDI+ is
    /// unavailable on this Linux host (no libgdiplus). The only caller
    /// (InfusionController) base64-encodes the PNG, so behavior is preserved.
    /// The icon overlay is skipped when iconSize is 0 (the only case used);
    /// a non-zero iconSize currently renders modules only.
    /// </summary>
    public static class QrEncoder
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="version">版本 1 ~ 40</param>
        /// <param name="pixel">像素点大小</param>
        /// <param name="iconPath">图标路径 (ignored when iconSize is 0)</param>
        /// <param name="iconSize">图标尺寸</param>
        /// <param name="iconBorder">图标边框厚度</param>
        /// <param name="whiteEdge">二维码白边 (quiet zone)</param>
        /// <returns>PNG bytes (grayscale)</returns>
        public static byte[] Code(string msg, int version, int pixel, string iconPath, int iconSize, int iconBorder, bool whiteEdge)
        {
            var codeGenerator = new QRCodeGenerator();
            var codeData = codeGenerator.CreateQrCode(msg, QRCodeGenerator.ECCLevel.M, true, true, QRCodeGenerator.EciMode.Utf8, version);
            var matrix = codeData.ModuleMatrix;
            // QRCoder's ModuleMatrix includes a 4-module quiet zone on each
            // side; the original GetGraphic(drawQuietZones: whiteEdge) only
            // rendered it when whiteEdge was true, so crop it otherwise.
            int crop = whiteEdge ? 0 : 4;
            int modules = matrix.Count - crop * 2;
            int size = modules * pixel;
            var pixels = new byte[size * size];
            // default white (255)
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = 255;
            }
            for (int y = 0; y < modules; y++)
            {
                var row = (BitArray)matrix[y + crop];
                for (int x = 0; x < modules; x++)
                {
                    if (!row[x + crop])
                    {
                        continue;
                    }
                    for (int dy = 0; dy < pixel; dy++)
                    {
                        for (int dx = 0; dx < pixel; dx++)
                        {
                            int px = x * pixel + dx;
                            int py = y * pixel + dy;
                            pixels[py * size + px] = 0;
                        }
                    }
                }
            }
            return PngEncoder.EncodeGrayscale(pixels, size, size);
        }
    }
}
