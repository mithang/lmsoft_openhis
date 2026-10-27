using NewtouchHIS.Lib.Base;

namespace Newtouch.MR.ManageSystem.Infrastructure
{
    /// <summary>System constants (ported from FrameworkBase ConstantsBase).</summary>
    public class Constants
    {
        /// <summary>Top-level organize id (from appsettings BusinessConfig).</summary>
        public static string TopOrganizeId
        {
            get { return ConfigInitHelper.SysConfig.Top_OrganizeId; }
        }

        public static string AppId
        {
            get { return ConfigInitHelper.SysConfig.AppId; }
        }
    }
}
