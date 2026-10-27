using NewtouchHIS.Lib.Base.Security;
using NewtouchHIS.Lib.Base.Utilities;

namespace NewtouchHIS.Services
{
    public class RemoteTreatedHelper
    {
        /// <summary>
        /// Âm thanh/hình ảnhCuộc họpAPI
        /// </summary>
        protected static string OuterMediaMeettingAPIHost
        {
            get
            {
                string hostconfig = AppSettings.GetConfig("BusinessConfig:AppAPIHost:OuterMediaMeettingAPIHost");
                if (hostconfig.EndsWith("/"))
                {
                    return hostconfig;
                }
                return $"{hostconfig}/";
            }
        }

        /// <summary>
        /// Lấy người dùng âm thanh/hình ảnh Token
        /// </summary>
        public readonly static string OuterMediaTokenRoute = $"{OuterMediaMeettingAPIHost}chatroom/rest/rtc/token";
        /// <summary>
        /// Lấy âm thanh/hình ảnhCuộc họpĐịa chỉ
        /// </summary>
        public readonly static string RemoteTreatedMeettingRoute = $"chatroom/index.html";
        /// <summary>
        /// Nhà cung cấp,Công ty XinZhi:xinzhi
        /// </summary>
        public static string Organization = AppSettings.GetConfig("RemoteTreated:SKey") ?? "xinzhi";
        /// <summary>
        /// Secret Mật khẩu nhà cung cấp dùng
        /// </summary>
        static string Secret = AppSettings.GetConfig("RemoteTreated:Organization") ?? "8a8e2a0db12343f589d7721762b94490";
        static string AES_IV = "A-16-Byte-String";

        /// <summary>
        /// Lắp ráp header yêu cầu Header
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> HeaderItems()
        {
            Random random = new Random();
            int randomNumber = random.Next();
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "Nonce", randomNumber.ToString() },
                { "Organization", Organization },
                { "Timestamp",  GetTimeStamp(DateTime.Now).ToString() },
                { "Signature", "" },
            };
            dic["Signature"] = MD5Encrypt.Md5($"{dic["Nonce"]}{dic["Timestamp"]}{Secret}", 32, false);
            return dic;
        }
        /// <summary>
        /// LấyRtcuserid
        /// </summary>
        /// <param name="Rtcuserid"></param>
        /// <returns></returns>
        public static string GetRtcuser(string Rtcuserid)
        {
            var data = AESEncrypt.AESDecrypt(Rtcuserid, Secret, AES_IV);
            return data;
        }

        /// <summary>
        /// DateTimeChuyển thành timestamp
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dateTime)
        {
            // Thời gian bắt đầu
            DateTime startTime = new(1970, 1, 1, 8, 0, 0);
            // 10timestamp 10 chữ số
            long timeStamp = Convert.ToInt64(dateTime.Subtract(startTime).TotalSeconds);
            // 13timestamp 10 chữ số
            //long timeStamp = Convert.ToInt64(dateTime.Subtract(_dtStart).TotalMilliseconds);
            return timeStamp;
        }
        /// <summary>
        /// Chuyển timestamp thànhDateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(long timeStamp)
        {
            // Thời gian bắt đầu
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000000");
            return startTime.Add(new TimeSpan(lTime)).AddHours(8);
        }
    }
}
