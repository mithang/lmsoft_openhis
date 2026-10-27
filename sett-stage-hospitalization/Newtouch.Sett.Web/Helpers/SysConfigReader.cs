using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.Sett.Web.Helpers
{
    /// <summary>
    /// View helper replacing Framework Newtouch.Infrastructure.SysConfigReader.
    /// Reads Sys_Config via the ported SysConfigRepo (Sett DB, Base DB fallback).
    /// </summary>
    public static class SysConfigReader
    {
        private static string OrgId()
        {
            try
            {
                return OperatorProvider.GetCurrent()?.OrganizeId;
            }
            catch
            {
                return null;
            }
        }

        public static bool? Bool(string code)
        {
            return new SysConfigRepo().GetBoolValueByCode(code, OrgId());
        }

        public static bool? Bool(string code, bool defaultValue)
        {
            return new SysConfigRepo().GetBoolValueByCode(code, OrgId(), defaultValue);
        }

        public static string String(string code)
        {
            return new SysConfigRepo().GetValueByCode(code, OrgId());
        }

        public static string OrgReportLink(string reportCode)
        {
            // Report server is not part of this deployment; views render the link
            // empty rather than pointing at a dead host.
            return "";
        }
    }
}
