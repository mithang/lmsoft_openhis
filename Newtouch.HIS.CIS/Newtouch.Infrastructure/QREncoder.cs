using System.Drawing;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Bộ mã hóa mã QR
    /// </summary>
    public static class QrEncoder
    {
        /// <summary>
        /// Sinh mã QR
        /// </summary>
        /// <param name="msg">Thông tin</param>
        /// <param name="version">Phiên bản 1 ~ 40</param>
        /// <param name="pixel">Kích thước điểm ảnh</param>
        /// <param name="iconPath">Đường dẫn biểu tượng</param>
        /// <param name="iconSize">Kích thước biểu tượng</param>
        /// <param name="iconBorder">Độ dày viền biểu tượng</param>
        /// <param name="whiteEdge">Viền trắng của mã QR</param>
        /// <returns>Ảnh bitmap</returns>
        public static Bitmap Code(string msg, int version, int pixel, string iconPath, int iconSize, int iconBorder, bool whiteEdge)
        {
            var codeGenerator = new QRCoder.QRCodeGenerator();
            var codeData = codeGenerator.CreateQrCode(msg, QRCoder.QRCodeGenerator.ECCLevel.M/* Đặt một cấp độ dung sai tại đây */, true, true, QRCoder.QRCodeGenerator.EciMode.Utf8, version);
            var code = new QRCoder.QRCode(codeData);
            var icon = new Bitmap(iconPath);
            var bmp = code.GetGraphic(pixel, Color.Black, Color.White, icon, iconSize, iconBorder, whiteEdge);
            return bmp;
        }
    }
}
