using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Net.Http;
using System.Text;

namespace Newtouch.EMR.Web.Helpers
{
    /// <summary>
    /// Local replacement for the FrameworkBase binary AuthenManageHelper /
    /// AuthenTokenHelper (cross-system API client used by the medical-record
    /// apply + MRQC-score actions). Behaviour:
    /// - appId / accessAppId: "EMR" (this app's id; the original read them from
    ///   the binary auth helper's config, which has no Core equivalent).
    /// - SiteConmonAPIHost: from appsettings AppSettings:SiteConmonAPIHost when
    ///   present, otherwise empty — calls then fail fast with a clear error
    ///   instead of silently posting nowhere. The common-API host does not
    ///   exist in this deployment (same situation as MRQC's 8015/8016 center
    ///   hosts), so these two actions report the connection failure; the
    ///   record pages themselves are unaffected.
    /// - GetToken: the auth-center token endpoint is likewise absent, so it
    ///   returns an empty token; HttpPost sends it as a Bearer token only
    ///   when non-empty.
    /// </summary>
    public static class AuthenManageHelper
    {
        public static string appId
        {
            get { return "EMR"; }
        }

        public static string accessAppId
        {
            get { return "EMR"; }
        }

        public static string SiteConmonAPIHost
        {
            get { return EmrConfig.GetAppConfigValue("SiteConmonAPIHost"); }
        }

        public static string GetToken()
        {
            return "";
        }

        public static AuthenApiResult<T> HttpPost<T>(string json, string url, OperatorModel user)
        {
            var text = PostJson(url, json, null);
            return JsonConvert.DeserializeObject<AuthenApiResult<T>>(text);
        }

        public static string HttpPost(string json, string url, string token)
        {
            return PostJson(url, json, token);
        }

        private static string PostJson(string url, string json, string token)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new InvalidOperationException(
                    "Chưa cấu hình địa chỉ API dùng chung (SiteConmonAPIHost) nên không thể gọi API liên hệ thống.");
            }
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            using var content = new StringContent(json ?? "", Encoding.UTF8, "application/json");
            using var response = client.PostAsync(url, content).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
    }

    public class AuthenApiResult<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }

    public class AuthenTokenHelper
    {
        public AuthenBusData BusData { get; set; }
    }

    public class AuthenBusData
    {
        public object data { get; set; }
    }
}
