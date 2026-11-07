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
        /// Tổ chức Id(đỉnhcấp,Hệ thốngcấu hình)
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                return AppConfig.TopOrganizeId;
            }
        }

        /// <summary>
        /// Tổ chức Id(đỉnhcấp,Hệ thốngcấu hình)
        /// </summary>
        public static string AppId
        {
            get
            {
                return AppConfig.AppId;
            }
        }

        /// <summary>
        /// Lấyhiện tạicủathuốcliều Khoa phòng(Nhà thuốchoặc Kho thuốc)
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
        /// Lấyhiện tạicủathuốcliều Khoa phòng(Nhà thuốchoặc Kho thuốc)
        /// </summary>
        /// <param name="userId"></param>
        public static LoginUserCurrentYfbmModel GetCurrentYfbm(string userId)
        {
            return RedisHelper.Get<LoginUserCurrentYfbmModel>(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId)) ?? new LoginUserCurrentYfbmModel();
        }

        /// <summary>
        /// Thời gianđịnh dạnghóa
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Quay lạinhấtnhỏ Thời gian 1970-01-01 00:00:000
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
