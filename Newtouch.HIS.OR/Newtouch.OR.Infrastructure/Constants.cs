
using NewtouchHIS.Lib.Base;

namespace Newtouch.OR.ManageSystem.Infrastructure
{
    /// <summary>
    /// Hằng số hệ thống
    /// </summary>
    public class Constants
    {

        /// <summary>
        /// Tổ chứcId(Cấp cao nhất,Cấu hình hệ thống)
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                return ConfigInitHelper.SysConfig.Top_OrganizeId;
            }
        }

        /// <summary>
        /// Tổ chứcId(Cấp cao nhất,Cấu hình hệ thống)
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