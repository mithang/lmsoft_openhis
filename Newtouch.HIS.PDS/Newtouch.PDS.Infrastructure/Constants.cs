using Newtouch.Core.Common.Utils;
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
        /// Tổ chức cấp cao nhấtId
        /// </summary>
        private static string _topOrganizeId;

        /// <summary>
        /// Tổ chứcId(Cấp cao nhất,Cấu hình hệ thống)
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
                    throw new Exception("Top_OrganizeIdChưa cấu hình");
                }
                return _topOrganizeId;
            }
        }

        /// <summary>
        /// Ứng dụngId
        /// </summary>
        private static string _AppId;

        /// <summary>
        /// Tổ chứcId(Cấp cao nhất,Cấu hình hệ thống)
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
                        //throw new Exception("AppIdChưa cấu hình");
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
            return RedisHelper.Get<LoginUserCurrentYfbmModel>(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId)) ?? new LoginUserCurrentYfbmModel();
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
            if (value == null)
            {
                RedisHelper.Remove(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId));
            }
            else
            {
                RedisHelper.Set(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId), value);
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
        /// Y lệnhID
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
