using NewtouchHIS.Lib.Base;
using Newtouch.Tools;
using NewtouchHIS.Lib.Base.Extension;
using Newtonsoft.Json.Linq;
using System;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Newtouch.Tools
{
    /// <summary>
    /// Minimal shims for the binary-only Newtouch.Tools helpers used by ported code.
    /// Same type/member names so ported call sites compile unchanged.
    /// </summary>
    public static class Json
    {
        public static string ToJson(object obj)
        {
            return obj.ToJson();
        }

        public static JObject ToJObject(string json)
        {
            return JObject.Parse(json);
        }

        public static JObject ToJObject(object obj)
        {
            return JObject.Parse(obj.ToJson());
        }
    }

    public static class ConfigurationHelper
    {
        /// <summary>Set once at startup (Program.cs) so static call sites can read config.</summary>
        public static Microsoft.Extensions.Configuration.IConfiguration Configuration { get; set; }

        public static string GetAppConfigValue(string key)
        {
            var v = Configuration?["BusinessConfig:" + key] ?? Configuration?[key];
            if (!string.IsNullOrWhiteSpace(v))
            {
                return v;
            }
            var sys = ConfigInitHelper.SysConfig;
            if (sys != null)
            {
                var prop = sys.GetType().GetProperty(key, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (prop != null)
                {
                    return prop.GetValue(sys)?.ToString();
                }
            }
            return null;
        }

        public static bool GetAppConfigBoolValue(string key, bool defaultValue = false)
        {
            var v = GetAppConfigValue(key);
            return bool.TryParse(v, out var b) ? b : defaultValue;
        }

        public static int GetAppConfigIntValue(string key, int defaultValue = 0)
        {
            var v = GetAppConfigValue(key);
            return int.TryParse(v, out var i) ? i : defaultValue;
        }
    }
}
