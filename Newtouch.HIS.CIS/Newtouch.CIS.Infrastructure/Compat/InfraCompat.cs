using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Newtouch.CIS.Compat
{
    /// <summary>Stand-in for Newtouch.CIS.Compat.FailedException.</summary>
    public class FailedCodeException : FailedException
    {
        public FailedCodeException(string message) : base(message) { }
        public FailedCodeException(string code, string message) : base(code, message) { }
    }

    public class FailedException : Exception
    {
        public FailedException(string message) : base(message)
        {
        }

        public FailedException(string message, Exception inner) : base(message, inner)
        {
        }

        public FailedException(string code, string message) : base(message)
        {
            Code = code;
        }

        public string Code { get; }
        public string Msg { get { return Message; } }
    }

    /// <summary>
    /// Static application configuration (replaces Framework web.config
    /// ConfigurationManager.AppSettings). Populated once at startup from
    /// appsettings.json via Program.cs. Missing cross-host keys read as "" so
    /// Site*APIHelper calls fail fast instead of crashing on null.
    /// </summary>
    public static class AppConfig
    {
        public static string TopOrganizeId { get; private set; } = "";
        public static string AppId { get; private set; } = "";
        public static string Token_ApiManage { get; private set; } = "";
        public static string SiteTokenAPIHost { get; private set; } = "";
        public static string SiteConmonAPIHost { get; private set; } = "";
        public static string SiteBaseAPIHost { get; private set; } = "";
        public static string SiteSettAPIHost { get; private set; } = "";
        public static string SiteHerpAPIHost { get; private set; } = "";
        public static string SiteORAPIHost { get; private set; } = "";
        public static string SiteYfykAPIHost { get; private set; } = "";
        public static string RedisConnection { get; private set; } = "127.0.0.1:6379";

        private static IConfiguration _config;

        public static void Init(IConfiguration cfg)
        {
            _config = cfg;
            TopOrganizeId = cfg["BusinessConfig:Top_OrganizeId"] ?? "";
            AppId = cfg["BusinessConfig:AppId"] ?? "";
            Token_ApiManage = cfg["Token_ApiManage"] ?? "";
            SiteTokenAPIHost = cfg["BusinessConfig:AppAPIHost:SiteAuthCenterHost"] ?? "";
            SiteConmonAPIHost = cfg["BusinessConfig:AppAPIHost:SiteApiManageHost"] ?? "";
            SiteBaseAPIHost = cfg["BusinessConfig:AppAPIHost:SiteBaseAPIHost"] ?? "";
            SiteSettAPIHost = cfg["BusinessConfig:AppAPIHost:SiteSettAPIHost"] ?? "";
            SiteHerpAPIHost = cfg["BusinessConfig:AppAPIHost:SiteHerpAPIHost"] ?? "";
            SiteORAPIHost = cfg["BusinessConfig:AppAPIHost:SiteORAPIHost"] ?? "";
            SiteYfykAPIHost = cfg["BusinessConfig:AppAPIHost:SiteYfykAPIHost"] ?? "";
            RedisConnection = cfg["RedisConfig:Connection"] ?? RedisConnection;
        }

        /// <summary>Host lookup by the legacy web.config key name.</summary>
        public static string Get(string key)
        {
            return key switch
            {
                "SiteTokenAPIHost" => SiteTokenAPIHost,
                "SiteConmonAPIHost" => SiteConmonAPIHost,
                "SiteBaseAPIHost" => SiteBaseAPIHost,
                "SiteSettAPIHost" => SiteSettAPIHost,
                "SiteHerpAPIHost" => SiteHerpAPIHost,
                "SiteORAPIHost" => SiteORAPIHost,
                "SiteYfykAPIHost" => SiteYfykAPIHost,
                "AppId" => AppId,
                "Token_ApiManage" => Token_ApiManage,
                _ => _config?[key] ?? "",
            };
        }
    }

    /// <summary>
    /// Stand-in for Newtouch.Tools.Net.HttpMethods (sync JSON POST helpers).
    /// Internal HIS hosts are loopback in this deployment, so the handler
    /// bypasses any proxy for them.
    /// </summary>
    public static class HttpMethods
    {
        private static readonly HttpClient _client;

        static HttpMethods()
        {
            var handler = new HttpClientHandler { UseProxy = false };
            _client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(30) };
        }

        public static string HttpPost(string url, string json, object headers)
        {
            using var content = new StringContent(json ?? "", Encoding.UTF8, "application/json");
            using var resp = _client.PostAsync(url, content).GetAwaiter().GetResult();
            return resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public static string HttpPostWithToken(string url, string json, string token)
        {
            using var content = new StringContent(json ?? "", Encoding.UTF8, "application/json");
            using var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            if (!string.IsNullOrWhiteSpace(token))
            {
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            using var resp = _client.SendAsync(req).GetAwaiter().GetResult();
            return resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public static string HttpGet(string url)
        {
            using var resp = _client.GetAsync(url).GetAwaiter().GetResult();
            return resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
    }

    /// <summary>
    /// Stand-in for Newtouch.Core.Redis.RedisHelper, backed by
    /// StackExchange.Redis against the deployment Redis (see appsettings
    /// RedisConfig:Connection). Only the methods used by CIS code are kept.
    /// </summary>
    public static class RedisHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> _conn =
            new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(AppConfig.RedisConnection),
                LazyThreadSafetyMode.ExecutionAndPublication);

        private static IDatabase Db => _conn.Value.GetDatabase();

        public static string StringGet(string key)
        {
            var val = Db.StringGet(key);
            return val.HasValue ? val.ToString() : null;
        }

        public static bool StringSet(string key, string value, TimeSpan expiry)
        {
            return Db.StringSet(key, value, expiry);
        }

        public static T Get<T>(string key)
        {
            var val = Db.StringGet(key);
            if (!val.HasValue)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(val.ToString());
        }

        public static bool KeyExpire(string key, TimeSpan expiry)
        {
            return Db.KeyExpire(key, expiry);
        }

        public static bool KeyDelete(string key)
        {
            return Db.KeyDelete(key);
        }
    }

    /// <summary>
    /// Stand-in for Newtouch.Infrastructure.Log.LogCore. Log-only surface;
    /// writes to System.Diagnostics.Trace (the Web app's own logging pipeline
    /// is separate). Signatures match the Monitoring/PerformanceMonitoring
    /// call sites.
    /// </summary>
    public static class LogCore
    {
        public static void Info(string title, string message, object tags = null)
        {
            Trace.WriteLine($"[INFO] {title}: {message}");
        }

        public static void Fatal(string title, string message, object addInfo = null) { System.Diagnostics.Trace.WriteLine("[FATAL] " + title + ": " + message); }
        public static void Fatal(string title, Exception ex, object addInfo = null) { System.Diagnostics.Trace.WriteLine("[FATAL] " + title + ": " + ex); }
        public static void Error(string message) { System.Diagnostics.Trace.WriteLine("[ERROR] " + message); }
        public static void Error(string title, string message, object addInfo = null) { System.Diagnostics.Trace.WriteLine("[ERROR] " + title + ": " + message); }
        public static void Error(string title, Exception ex, object addInfo = null)
        {
            Trace.WriteLine($"[ERROR] {title}: {ex}");
        }
    }

    /// <summary>
    /// Stand-in for FrameworkBase.MultiOrg.Infrastructure.SiteAPIRequestHelperBase.
    /// The original base resolved the target host from the derived class's
    /// private static _domainConfigKey field and POSTed JSON (with the API
    /// token) to host + api path. Target hosts (Sett/Herp/OR/Yfyk center
    /// APIs) do not exist in this Linux deployment, so a failed call returns
    /// default(T) and the caller reports it; pages that do not need a
    /// cross-host call are unaffected.
    /// </summary>
    public class SiteAPIRequestHelperBase<TSelf>
    {
        public static T Request<T>(string api, object reqObj, bool autoAppendToken = true)
        {
            try
            {
                var keyField = typeof(TSelf).GetField("_domainConfigKey",
                    BindingFlags.NonPublic | BindingFlags.Static);
                var host = keyField != null
                    ? AppConfig.Get(keyField.GetValue(null) as string)
                    : "";
                if (string.IsNullOrWhiteSpace(host))
                {
                    return default;
                }
                var url = host.TrimEnd('/') + "/" + api.TrimStart('/');
                var json = JsonConvert.SerializeObject(reqObj);
                string token = null;
                if (!autoAppendToken) goto skipToken;
                try
                {
                    token = Newtouch.Infrastructure.AuthenManageHelper.GetToken();
                }
                catch
                {
                    token = null;
                }
                skipToken:
                var resp = string.IsNullOrWhiteSpace(token)
                    ? HttpMethods.HttpPost(url, json, null)
                    : HttpMethods.HttpPostWithToken(url, json, token);
                if (string.IsNullOrWhiteSpace(resp))
                {
                    return default;
                }
                return JsonConvert.DeserializeObject<T>(resp);
            }
            catch
            {
                return default;
            }
        }
    }

    /// <summary>API-center response envelope (from AuthenManageHelper).</summary>
    public class ApiManageResponse<T>
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData<T> BusData { get; set; }
    }

    public class BusData<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }
}
