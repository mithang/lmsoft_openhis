using Newtouch.Core.Common.Utils;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Infrastructure.Model;
using System;
using Newtouch.Tools;
using System.IO;
using System.Net;


namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class Constants
    {

        /// <summary>
        /// Tổ chức cấp cao nhất Id
        /// </summary>
        private static string _topOrganizeId;

        /// <summary>
        /// Tổ chức Id(Cấp cao nhất,Cấu hình hệ thống)
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                if (_topOrganizeId != null)
                {
                    return _topOrganizeId;
                }
                _topOrganizeId = ConfigurationHelper.GetAppConfigValue("Top_OrganizeId");
                if (string.IsNullOrWhiteSpace(_topOrganizeId))
                {
                    throw new Exception("Top_OrganizeId Chưa cấu hình");
                }
                return _topOrganizeId;
            }
        }

        /// <summary>
        /// Ứng dụng Id
        /// </summary>
        private static string _AppId;

        /// <summary>
        /// Tổ chức Id(Cấp cao nhất,Cấu hình hệ thống)
        /// </summary>
        public static string AppId
        {
            get
            {
                if (_AppId == null)
                {
                    _AppId = ConfigurationHelper.GetAppConfigValue("AppId") as string;
                    if (string.IsNullOrWhiteSpace(_AppId))
                    {
                        //throw new Exception("AppId Chưa cấu hình");
                    }
                }
                return _AppId;
            }
        }
        public static string _AddressIp;
        public static string AddressIp
        {

            get
            {
                if (_AddressIp == null)
                {
                    foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                    {
                        if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                        {
                            _AddressIp = _IPAddress.ToString();
                        }
                    }
                }
                //Info("project2:" + _AddressIp);
                return _AddressIp;
            }
        }
        public static void Info(string message)
        {
            try
            {
                var root = "C:\\HISLog\\log_yibao";
                var date = DateTime.Now.ToString("yyyyMMddHHmm");
                var dirPath = string.Format("{0}\\{1}", root, date.Substring(0, 8));
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                var filePath = string.Format("{0}\\{1}.txt", dirPath, date.Substring(8, 2));
                File.AppendAllText(filePath, string.Format("\r\n\r\n{0}.Info.{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
            }
            catch { }
        }

        #region Thông tin bộ phận nhà thuốc hiện tại của người dùng đăng nhập Không bao giờ hết hạn Chia sẻ giữa các hệ thống khác nhau
        // NX31-A5 (6): Redis THẬT (NewtouchHIS.Lib.Base.Utilities.RedisHelper, mẫu
        // OperatorProvider.GetCurrent) — trước đây RedisHelper ở đây resolve về
        // Newtouch.Tools.RedisHelper trong Shims/MissingShims2.cs, chỉ là
        // ConcurrentDictionary in-memory theo process: PDS Web và PDS API mỗi
        // process một bản, mất khi restart, không chia sẻ được. Giờ Web + API
        // (cùng khởi tạo singleton Redis thật qua AddWebServiceBase, chung
        // RedisConfig 127.0.0.1:6379 Db 0) đọc chung một key. Key giữ nguyên
        // "string:CurrentYfbmInfo_{userId+AddressIp}", chỉ thêm prefix OHIS:
        // GetString/SetString/Remove đều thêm prefix giống nhau nên nhất quán.
        // Không đặt expiry (giữ ngữ nghĩa "không bao giờ hết hạn" cũ).
        // LƯU Ý: Redis down thì Get trả rỗng như key chưa có (fail-soft như cũ);
        // nhưng Redis down vốn đã làm login hỏng nên PDS cũng không dùng được.

        /// <summary>
        /// Lấy bộ phận dược hiện tại(Nhà thuốc hoặc kho thuốc)
        /// </summary>
        public static LoginUserCurrentYfbmModel CurrentYfbm
        {
            get
            {
                var operate = OperatorProvider.GetCurrent();
                return GetCurrentYfbm(operate.UserId);
            }
        }

        /// <summary>
        /// Lấy bộ phận dược hiện tại(Nhà thuốc hoặc kho thuốc)
        /// </summary>
        /// <param name="userId"></param>
        public static LoginUserCurrentYfbmModel GetCurrentYfbm(string userId)
        {
            userId = userId + AddressIp;
            try
            {
                var json = NewtouchHIS.Lib.Base.Utilities.RedisHelper.GetString(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId));
                if (string.IsNullOrWhiteSpace(json)) return new LoginUserCurrentYfbmModel();
                return json.ToObject<LoginUserCurrentYfbmModel>() ?? new LoginUserCurrentYfbmModel();
            }
            catch
            {
                return new LoginUserCurrentYfbmModel();
            }
        }

        /// <summary>
        /// Đặt bộ phận dược hiện tại(Nhà thuốc hoặc kho thuốc)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        public static void SetCurrentYfbm(string userId, LoginUserCurrentYfbmModel value)
        {
            //Tránh máy tính khác đăng nhập làm thay đổi vai trò nhà thuốc của thiết bị hiện tại
            userId = userId + AddressIp;
            var key = string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId);
            if (value == null)
            {
                NewtouchHIS.Lib.Base.Utilities.RedisHelper.Remove(key);
            }
            else
            {
                NewtouchHIS.Lib.Base.Utilities.RedisHelper.SetString(key, value.ToJson());
            }
        }

        /// <summary>
        /// Trả về thời gian nhỏ nhất 1970-01-01 00:00:000
        /// </summary>
        public static DateTime MinDateTime
        {
            get { return Convert.ToDateTime("1970-01-01"); }
        }

        #endregion

        /// <summary>
        /// Định dạng thời gian
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";


        #region log tags

        /// <summary>
        /// Số thứ tự khách hàng
        /// </summary>
        public const string ClientNo = "clientNo";
        /// <summary>
        /// Y lệnh ID
        /// </summary>
        public const string ZyId = "zxId";
        /// <summary>
        /// Mã dược phẩm
        /// </summary>
        public const string YpCode = "ypCode";
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public const string Cfh = "Cfh";
        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public const string Cfnm = "Cfnm";
        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public const string OrganizeId = "OrganizeId";
        /// <summary>
        /// Bộ phận nhà thuốc
        /// </summary>
        public const string Yfbm = "Yfbm";
        /// <summary>
        /// Nhân viên thao tác hiện tại
        /// </summary>
        public const string CreatorCode = "CreatorCode";
        #endregion
    }
}
