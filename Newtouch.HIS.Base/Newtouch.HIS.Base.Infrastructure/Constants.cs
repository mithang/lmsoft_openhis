using NewtouchHIS.Lib.Base;
using Newtouch.Tools;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Application constants ported from the Framework version.
    /// Values now come from the net6.0 config system (BusinessConfig in
    /// appsettings.json) instead of ConfigurationManager.AppSettings.
    /// </summary>
    public static class Constants
    {
        public static string TopOrganizeId
        {
            get
            {
                var v = ConfigInitHelper.SysConfig?.Top_OrganizeId;
                if (string.IsNullOrWhiteSpace(v))
                {
                    throw new System.Exception("Top_OrganizeId is not configured (BusinessConfig.Top_OrganizeId)");
                }
                return v;
            }
        }

        public static string AppId => ConfigInitHelper.SysConfig?.AppId;

        /// <summary>
        /// Default password for newly created users (original: appSetting
        /// LoginUser_Default_Password, "123456" in the shipped HOSP system.config).
        /// Throws when unconfigured, matching the original behavior.
        /// </summary>
        public static string LogonDefaultPassword
        {
            get
            {
                var v = Newtouch.Tools.ConfigurationHelper.GetAppConfigValue("LoginUser_Default_Password");
                if (string.IsNullOrWhiteSpace(v))
                {
                    throw new System.ApplicationException("LoginUser_Default_Password is not configured");
                }
                return v;
            }
        }
    }
}
