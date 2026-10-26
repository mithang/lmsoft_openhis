using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Lớp trợ giúp
    /// </summary>
    public class CommmHelper
    {
        /// <summary>
        /// Hạng mục thu phí, lượng điều trị quy đổi thành số lượng
        /// </summary>
        /// <param name="zll">Chỉ tổng lượng của trang đơn thuốc phục hồi chức năng</param>
        /// <param name="dwjls"></param>
        /// <param name="jjcl">Enum EnumSfxmJjcl</param>
        /// <param name="throwExWhenZero">Khi kết quả số lượng bằng 0 có ném ngoại lệ hay không, mặc định ném</param>
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
        /// Lấy đường dẫn nội bộ tương ứng với tệp nội bộ
        /// </summary>
        /// <param name="path">Đường dẫn tương đối</param>
        /// <returns>Trả về đường dẫn tuyệt đối</returns>
        public static string GetLocalFilePath(string path)
        {
            var configFileExportBaseDir = ConfigurationHelper.GetAppConfigValue("LocalFileBaseDir");
            if (string.IsNullOrWhiteSpace(configFileExportBaseDir))
            {
                configFileExportBaseDir = "D:\\";
            }
            var filePath = (configFileExportBaseDir + path).Replace("\\\\", "\\");

            var iIndex = filePath.LastIndexOf("\\");
            var dirPath = filePath.Substring(0, iIndex);
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }

            return filePath;
        }

        /// <summary>
        /// Sinh mã vạch một chiều
        /// </summary>
        /// <param name="text">Nội dung</param>
        /// <param name="width">Chiều rộng</param>
        /// <param name="height">Chiều cao</param>
        /// <returns></returns>
        public static string GenerateBarCode(string text, int width, int height)
        {
            byte[] imageArray;
            //System.IO.MemoryStream MStream = new System.IO.MemoryStream();
            BarcodeWriter writer = new BarcodeWriter();
            //Dùng định dạng ITF, không thể quét được bằng Alipay, WeChat thường dùng hiện nay
            //Nếu muốn sinh mã có thể nhận dạng thì có thể dùng định dạng CODE_128
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = 2
            };
            writer.Options = options;
            Image map = writer.Write(text);
            using (System.IO.MemoryStream MStream = new System.IO.MemoryStream())
            {
                map.Save(MStream, ImageFormat.Png);
                imageArray = new byte[MStream.Length];
                MStream.Seek(0, System.IO.SeekOrigin.Begin);
                MStream.Read(imageArray, 0,(int)MStream.Length);
            }
            return Convert.ToBase64String(imageArray);
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
