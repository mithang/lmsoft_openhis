using NewtouchHIS.Lib.Base;

namespace Newtouch.MRQC.Infrastructure
{
    /// <summary>
    /// System constants. Values come from appsettings BusinessConfig
    /// (set by AddWebServiceBase), same as the net6.0 SSO app.
    /// </summary>
    public class Constants
    {
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
