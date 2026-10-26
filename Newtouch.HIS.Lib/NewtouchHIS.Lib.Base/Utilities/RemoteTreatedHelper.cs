using NewtouchHIS.Lib.Base.Security;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public class RemoteTreatedHelper
    {
        /// <summary>
        /// nhà cung cấp，công ty Newtouch：xinzhi
        /// </summary>
        public static string Organization = AppSettings.GetConfig("RemoteTreated:SKey") ?? "xinzhi";
        /// <summary>
        /// Secret là mật khẩu dùng cho nhà cung cấp
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
        /// DateTimechuyển thành dấu thời gian
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dateTime)
        {
            // thời gian bắt đầu
            DateTime startTime = new(1970, 1, 1, 8, 0, 0);
            // 10dấu thời gian dạng bit
            long timeStamp = Convert.ToInt64(dateTime.Subtract(startTime).TotalSeconds);
            // 13dấu thời gian dạng bit
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
