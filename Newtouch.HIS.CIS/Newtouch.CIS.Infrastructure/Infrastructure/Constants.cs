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
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                return AppConfig.TopOrganizeId;
            }
        }

        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string AppId
        {
            get
            {
                return AppConfig.AppId;
            }
        }

        /// <summary>
        /// 获取当前的药剂部门（药房或药库）
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
        /// 获取当前的药剂部门（药房或药库）
        /// </summary>
        /// <param name="userId"></param>
        public static LoginUserCurrentYfbmModel GetCurrentYfbm(string userId)
        {
            return RedisHelper.Get<LoginUserCurrentYfbmModel>(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId)) ?? new LoginUserCurrentYfbmModel();
        }

        /// <summary>
        /// 时间格式化
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 返回最小时间 1970-01-01 00:00:000
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
