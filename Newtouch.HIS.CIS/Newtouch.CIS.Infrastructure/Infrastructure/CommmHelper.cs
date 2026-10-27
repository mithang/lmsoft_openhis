using Newtouch.CIS.Compat;

using System;
using System.IO;
using ZXing;
using ZXing.Common;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public class CommmHelper
    {
        /// <summary>
        /// 收费项目 治疗量 换算成 数量
        /// </summary>
        /// <param name="zll">指的是康复处方页的总量</param>
        /// <param name="dwjls"></param>
        /// <param name="jjcl">枚举EnumSfxmJjcl</param>
        /// <param name="throwExWhenZero">当数量结果为0时 是否抛异常，默认抛</param>
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
        /// 获取本地文件对应的本地路径
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>返回绝对路径</returns>
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
        /// 生成一维条形码
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static string GenerateBarCode(string text, int width, int height)
        {
            // GDI+-free: ZXing PixelData (8-bit grayscale) -> pure-managed PNG.
            // System.Drawing is unavailable on this Linux host (no libgdiplus),
            // so the original Bitmap.Save path cannot run here.
            var writer = new BarcodeWriterPixelData();
            //使用ITF 格式，不能被现在常用的支付宝、微信扫出来
            //如果想生成可识别的可以使用 CODE_128 格式
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
