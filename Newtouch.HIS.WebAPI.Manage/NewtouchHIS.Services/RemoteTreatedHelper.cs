using NewtouchHIS.Lib.Base.Security;
using NewtouchHIS.Lib.Base.Utilities;

namespace NewtouchHIS.Services
{
    public class RemoteTreatedHelper
    {
        /// <summary>
        /// cuộc họp âm thanh – hình ảnhAPI
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
        /// lấy người dùng âm thanh – hình ảnh Token
        /// </summary>
        public readonly static string OuterMediaTokenRoute = $"{OuterMediaMeettingAPIHost}chatroom/rest/rtc/token";
        /// <summary>
        /// lấy địa chỉ cuộc họp âm thanh – hình ảnh
        /// </summary>
        public readonly static string RemoteTreatedMeettingRoute = $"chatroom/index.html";
        /// <summary>
        /// nhà sản xuất，新致公司：xinzhi
        /// </summary>
        public static string Organization = AppSettings.GetConfig("RemoteTreated:SKey") ?? "xinzhi";
        /// <summary>
        /// Secret mật khẩu sử dụng cho nhà sản xuất
        /// </summary>
        static string Secret = AppSettings.GetConfig("RemoteTreated:Organization") ?? "8a8e2a0db12343f589d7721762b94490";
        static string AES_IV = "A-16-Byte-String";

        /// <summary>
        /// lắp ráp tiêu đề yêu cầu Header
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
        /// lấyRtcuserid
        /// </summary>
        /// <param name="Rtcuserid"></param>
        /// <returns></returns>
        public static string GetRtcuser(string Rtcuserid)
        {
            var data = AESEncrypt.AESDecrypt(Rtcuserid, Secret, AES_IV);
            return data;
        }

        /// <summary>
        /// DateTimechuyển đổi dấu thời gian
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dateTime)
        {
            // thời gian bắt đầu
            DateTime startTime = new(1970, 1, 1, 8, 0, 0);
            // 10dấu thời gian của vị trí
            long timeStamp = Convert.ToInt64(dateTime.Subtract(startTime).TotalSeconds);
            // 13dấu thời gian của vị trí
            //long timeStamp = Convert.ToInt64(dateTime.Subtract(_dtStart).TotalMilliseconds);
            return timeStamp;
        }
        /// <summary>
        /// chuyển đổi dấu thời gianDateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(long timeStamp)
        {
            // thời gian bắt đầu
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000000");
            return startTime.Add(new TimeSpan(lTime)).AddHours(8);
        }
    }
}
