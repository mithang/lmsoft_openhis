using Newtouch.CIS.Compat;

using System;
using System.IO;
using ZXing;
using ZXing.Common;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// giúpgiúploại
    /// </summary>
    public class CommmHelper
    {
        /// <summary>
        /// Mục thu phí Điều trịlượng đổitínhthành Số lượng
        /// </summary>
        /// <param name="zll">chỉcủalàPhục hồi chức năngĐơn thuốctrangcủaTổng lượng</param>
        /// <param name="dwjls"></param>
        /// <param name="jjcl">Liệt kêEnumSfxmJjcl</param>
        /// <param name="throwExWhenZero">khiSố lượngkếtnếulà0khi làKhôngnémNgoại lệ，Mặc địnhném</param>
        /// <returns></returns>
        public static int CalcSfxmSl(int? mczll, int? dwjls, int? jjcl,
            bool throwExWhenZero = true)
        {
            if ((mczll ?? 0) <= 0)
            {
                throw new FailedException("zll is required");
            }
            if ((dwjls ?? 0) <= 0)
            {
                throw new FailedException("dwjls is required");
            }
            if ((jjcl ?? 0) <= 0)
            {
                throw new FailedException("jjcl is required");
            }
            var res = mczll.Value / dwjls.Value;
            if (res <= 0 && throwExWhenZero)
            {
                throw new FailedException("zero is not allowed");
            }
            return res;
        }

        /// <summary>
        /// Lấybảnđịavănmụctương ứngcủabảnđịaĐường dẫn
        /// </summary>
        /// <param name="path">nhauđốiĐường dẫn</param>
        /// <returns>Quay lại绝đốiĐường dẫn</returns>
        public static string GetLocalFilePath(string path)
        {
            var configFileExportBaseDir = AppConfig.Get("LocalFileBaseDir");
            if (string.IsNullOrWhiteSpace(configFileExportBaseDir))
            {
                // No Windows D:\ drive on Linux: fall back to the OS temp dir.
                configFileExportBaseDir = Path.GetTempPath();
            }
            var normalized = path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
            var filePath = Path.Combine(configFileExportBaseDir, normalized.TrimStart(Path.DirectorySeparatorChar));
            var dirPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(dirPath) && !Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            return filePath;
        }

        /// <summary>
        /// tạomột维dònghìnhmã
        /// </summary>
        /// <param name="text">nội dung</param>
        /// <param name="width">rộngđộ</param>
        /// <param name="height">caođộ</param>
        /// <returns></returns>
        public static string GenerateBarCode(string text, int width, int height)
        {
            // GDI+-free: ZXing PixelData (8-bit grayscale) -> pure-managed PNG.
            // System.Drawing is unavailable on this Linux host (no libgdiplus),
            // so the original Bitmap.Save path cannot run here.
            var writer = new BarcodeWriterPixelData();
            //Sử dụngITF định dạng，không thểbịhiệnởthường dùngcủaAlipay、WeChat扫rađến
            //nếunếunghĩtạocó thểbiếtloạicủacó thểlấySử dụng CODE_128 định dạng
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = 2
            };
            writer.Options = options;
            var pixelData = writer.Write(text);
            // PixelData.Pixels is 4 bytes per pixel (BGRA); collapse to
            // 1-byte grayscale (bars are pure black/white, channels agree).
            var gray = new byte[pixelData.Width * pixelData.Height];
            for (int i = 0; i < gray.Length; i++)
            {
                gray[i] = pixelData.Pixels[i * 4];
            }
            var png = PngEncoder.EncodeGrayscale(gray, pixelData.Width, pixelData.Height);
            return Convert.ToBase64String(png);
        }
    }

    public enum DateInterval
    {
        Second, Minute, Hour, Day, Week, Month, Quarter, Year
    }

    public sealed class DateTimeManger
    {

        private DateTimeManger()
        { }//end of default constructor

        public static long DateDiff(DateInterval Interval, System.DateTime StartDate, System.DateTime EndDate)
        {
            long lngDateDiffValue = 0;
            System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (Interval)
            {
                case DateInterval.Second:
                    lngDateDiffValue = (long)TS.TotalSeconds;
                    break;
                case DateInterval.Minute:
                    lngDateDiffValue = (long)TS.TotalMinutes;
                    break;
                case DateInterval.Hour:
                    lngDateDiffValue = (long)TS.TotalHours;
                    break;
                case DateInterval.Day:
                    lngDateDiffValue = (long)TS.Days;
                    break;
                case DateInterval.Week:
                    lngDateDiffValue = (long)(TS.Days / 7);
                    break;
                case DateInterval.Month:
                    lngDateDiffValue = (long)(TS.Days / 30);
                    break;
                case DateInterval.Quarter:
                    lngDateDiffValue = (long)((TS.Days / 30) / 3);
                    break;
                case DateInterval.Year:
                    lngDateDiffValue = (long)(TS.Days / 365);
                    break;
            }
            return (lngDateDiffValue);
        }//end of DateDiff

    }//end of class
}
