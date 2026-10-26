using System;
using Newtouch.Herp.Infrastructure.Model;
using Newtouch.Core.Redis;
using Newtouch.Common.Operator;

namespace Newtouch.Herp.Infrastructure
{
    /// <summary>
    /// Hằng số hệ thống
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
                _topOrganizeId = System.Configuration.ConfigurationManager.AppSettings["Top_OrganizeId"];
                if (string.IsNullOrWhiteSpace(_topOrganizeId))
                {
                    throw new Exception("Top_OrganizeId未配置");
                }
                return _topOrganizeId;
            }
        }

        /// <summary>
        /// Trả về thời gian nhỏ nhất 1970-01-01 00:00:000
        /// </summary>
        public static DateTime MinDateTime
        {
            get { return Convert.ToDateTime("1970-01-01"); }
        }

        /// <summary>
        /// Định dạng thời gian
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        #region tag

        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public const string OrganizeId = "OrganizeId";
        /// <summary>
        /// Kho ID
        /// </summary>
        public const string KfId = "KfId";
        /// <summary>
        /// Nhân viên thao tác hiện tại
        /// </summary>
        public const string CreatorCode = "CreatorCode";

        #endregion

        #region context info
        /// <summary>
        /// Ứng dụng Id
        /// </summary>
        private static string _appId;
        /// <summary>
        /// Tổ chức Id(Cấp cao nhất,Cấu hình hệ thống)
        /// </summary>
        public static string AppId
        {
            get
            {
                if (_appId != null) return _appId;
                _appId = System.Configuration.ConfigurationManager.AppSettings["AppId"];
                return _appId;
            }
        }

        /// <summary>
        /// Đặt kho hiện tại
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        public static void SetCurrentKf(string userId, LoginUserCurrentKfModel value)
        {
            if (value == null)
            {
                RedisHelper.Remove(string.Format(CacheKey.CurrentKfInfoEntityKey, userId));
            }
            else
            {
                RedisHelper.Set(string.Format(CacheKey.CurrentKfInfoEntityKey, userId), value);
            }
        }

        /// <summary>
        /// Lấy thông tin kho hiện tại
        /// </summary>
        /// <param name="userId"></param>
        private static LoginUserCurrentKfModel GetCurrentKf(string userId)
        {
            return RedisHelper.Get<LoginUserCurrentKfModel>(string.Format(CacheKey.CurrentKfInfoEntityKey, userId)) ?? new LoginUserCurrentKfModel();
        }

        /// <summary>
        /// Lấy thông tin kho hiện tại
        /// </summary>
        public static LoginUserCurrentKfModel CurrentKf
        {
            get
            {
                var operate = OperatorProvider.GetCurrent();
                return GetCurrentKf(operate.UserId);
            }
        }

        #endregion
    }
}