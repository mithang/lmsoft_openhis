using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.CIS.Web.Helpers
{
    /// <summary>Port of the binary FrameworkBase SiteUrl view helper.</summary>
    public static class SiteUrl
    {
        public static string SiteStaticDomain => NewtouchHIS.Lib.Base.Utilities.MvcHelper.SiteStaticDomain;
        public static string GetUrl(string key, string defaultValue)
        {
            return Newtouch.CIS.Compat.AppConfig.Get(key) ?? defaultValue;
        }
        public static string GetStaticResourceScriptUrl(string url, bool isInSiteStatic = true)
        {
            return NewtouchHIS.Lib.Base.Utilities.MvcHelper.GetStaticResourceScriptUrl(url, isInSiteStatic);
        }
    }

    /// <summary>
    /// Best-effort port of the binary FrameworkBase SysConfigReader view helper.
    /// Reads NewtouchHIS_Base.Sys_GlobalConfig (zt='1'); any failure returns
    /// the safe default so a view never crashes on config lookup. The demo DB
    /// has an empty Sys_GlobalConfig, so defaults are the normal path there.
    /// OrgReportLink is a stub over the same table: the original report-link
    /// mapping table is not in CIS/Base source.
    /// </summary>
    public static class SysConfigReader
    {
        private static string GetValue(string code)
        {
            try
            {
                var row = NewtouchHIS.Lib.DataBaseSvr.SqlSugarDbContext.Db.GetConnection("BaseDb")
                    .Ado.SqlQuerySingle<Newtouch.HIS.Domain.Entity.SysGlobalConfigEntity>(
                        "SELECT * FROM Sys_GlobalConfig WHERE Code = @code AND zt = '1'",
                        new SqlSugar.SugarParameter("@code", code));
                return row?.Value;
            }
            catch
            {
                return null;
            }
        }

        public static string String(string code) => GetValue(code) ?? "";
        public static bool Bool(string code)
        {
            var v = GetValue(code);
            return v == "1" || string.Equals(v, "true", StringComparison.OrdinalIgnoreCase)
                || string.Equals(v, "ON", StringComparison.OrdinalIgnoreCase);
        }
        public static bool? Bool(string code, bool defaultValue)
        {
            var v = GetValue(code);
            if (string.IsNullOrWhiteSpace(v)) return defaultValue;
            return v == "1" || string.Equals(v, "true", StringComparison.OrdinalIgnoreCase)
                || string.Equals(v, "ON", StringComparison.OrdinalIgnoreCase);
        }
        public static string OrgReportLink(string code) => GetValue(code) ?? "";
    }

    /// <summary>Port of the binary enum -&gt; SelectList view helper.</summary>
    public static class EnumViewExtensions
    {
        public static IEnumerable<SelectListItem> ToDescSelectList(this Enum value)
        {
            var type = value.GetType();
            return Enum.GetValues(type).Cast<Enum>().Select(e => new SelectListItem
            {
                Value = Convert.ToInt32(e).ToString(),
                Text = e.GetDescription(),
                Selected = e.Equals(value)
            }).ToList();
        }
    }
}
