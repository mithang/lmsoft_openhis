
using NewtouchHIS.Lib.Base;

namespace Newtouch.OR.ManageSystem.Infrastructure
{
    /// <summary>
    /// 系统常量
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
                return ConfigInitHelper.SysConfig.Top_OrganizeId;
            }
        }

        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string AppId
        {
            get
            {
                return ConfigInitHelper.SysConfig.AppId;
            }
        }
    }
}