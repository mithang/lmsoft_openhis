using System;
using Newtouch.CIS.Compat;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Ported from Framework CIS Constants. TopOrganizeId / AppId now come
    /// from AppConfig (populated from appsettings.json at startup) instead
    /// of FrameworkBase ConstantsBase / web.config.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Tổ chức Id(đỉnh cấp,Hệ thống cấu hình)
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                return AppConfig.TopOrganizeId;
            }
        }

        /// <summary>
        /// Tổ chức Id(đỉnh cấp,Hệ thống cấu hình)
        /// </summary>
        public static string AppId
        {
            get
            {
                return AppConfig.AppId;
            }
        }

        /// <summary>
        /// Lấy hiện tại của thuốc liều Khoa phòng(Nhà thuốc hoặc Kho thuốc)
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
        /// Lấy hiện tại của thuốc liều Khoa phòng(Nhà thuốc hoặc Kho thuốc)
        /// </summary>
        /// <param name="userId"></param>
        public static LoginUserCurrentYfbmModel GetCurrentYfbm(string userId)
        {
            return RedisHelper.Get<LoginUserCurrentYfbmModel>(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId)) ?? new LoginUserCurrentYfbmModel();
        }

        /// <summary>
        /// Thời gian định dạng hóa
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Quay lại nhất nhỏ Thời gian 1970-01-01 00:00:000
        /// </summary>
        public static DateTime MinDateTime
        {
            get { return Convert.ToDateTime("1970-01-01"); }
        }

        public static string DeptCode
        {
            get
            {
                var operate = OperatorProvider.GetCurrent();
                return operate.DepartmentCode;
            }
        }
    }
}
