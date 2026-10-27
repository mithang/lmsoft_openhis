using Newtonsoft.Json;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Operator;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Infrastructure
{
    /// <summary>
    /// Center-API (ApiManage/AuthCenter) response envelope, as returned by the
    /// vendor API center. BusData carries the vendor result code (10000 = ok).
    /// </summary>
    public class BusData<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }

    public class ApiManageResponse<T>
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData<T> BusData { get; set; }
    }

    public class AuthenBusData
    {
        public object data { get; set; }
    }

    public class AuthenTokenHelper
    {
        public AuthenBusData BusData { get; set; }
    }

    public class Tokendata
    {
        public string Token { get; set; }
    }

    /// <summary>
    /// Port of the Framework AuthenManageHelper to HttpClient.
    /// Differences from the original: the app token is cached in memory
    /// (the original used Redis) and all calls bypass the egress proxy
    /// (UseProxy=false) so loopback hosts are reachable.
    /// NOTE: the center hosts (SiteApiManageHost/SiteAuthCenterHost) do not
    /// exist in this Linux deployment, so every call through this helper
    /// fails with a connection error until those hosts are provided.
    /// </summary>
    public class AuthenManageHelper
    {
        public static string SiteTokenAPIHost
        {
            get { return ConfigInitHelper.SysConfig.AppAPIHost?.SiteAuthCenterHost; }
        }

        public static string SiteConmonAPIHost
        {
            get { return ConfigInitHelper.SysConfig.AppAPIHost?.SiteApiManageHost; }
        }

        public static string appId
        {
            get { return ConfigInitHelper.SysConfig.AppId; }
        }

        public static string accessAppId
        {
            get { return AppSettings.GetConfig("Token_ApiManage"); }
        }

        private static readonly HttpClient _http = new HttpClient(new HttpClientHandler { UseProxy = false });

        private static string _cachedToken = "";
        private static DateTime _cachedTokenExpiry = DateTime.MinValue;

        public static string GetToken()
        {
            if (!string.IsNullOrWhiteSpace(_cachedToken) && DateTime.Now < _cachedTokenExpiry)
            {
                return _cachedToken;
            }
            if (string.IsNullOrWhiteSpace(SiteTokenAPIHost))
            {
                throw new FailedException("[Xác thực] Chưa cấu hình SiteAuthCenterHost");
            }
            var reqObj = new { AppId = appId, Data = accessAppId };
            var resp = HttpPostRaw(SiteTokenAPIHost + "api/Auth/GetAppToken", reqObj.ToJson(), null).GetAwaiter().GetResult();
            var tokenResp = JsonConvert.DeserializeObject<AuthenTokenHelper>(resp);
            var data = JsonConvert.DeserializeObject<Tokendata>(tokenResp.BusData.data.ToString());
            _cachedToken = data.Token;
            _cachedTokenExpiry = DateTime.Now.AddMinutes(20);
            return _cachedToken;
        }

        public static string HttpPost(string data, string uri, string token)
        {
            return HttpPostRaw(uri, data, token).GetAwaiter().GetResult();
        }

        public static BusData<T> HttpPost<T>(string data, string uri, OperatorModel user)
        {
            string token = "";
            try
            {
                token = OperatorProvider.GetCurrentToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new FailedException("[Xác thực] Không lấy được mã định danh Api, vui lòng liên hệ quản trị viên");
                }
                string response = HttpPostRaw(uri, data, token).GetAwaiter().GetResult();
                ApiManageResponse<T> apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                if (apiResp.HttpStatus == (int)HttpStatusCode.Unauthorized)
                {
                    // auth failed: refresh the app token and retry once
                    _cachedToken = "";
                    token = GetToken();
                    response = HttpPostRaw(uri, data, token).GetAwaiter().GetResult();
                    apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                }
                if (apiResp.HttpStatus != (int)HttpStatusCode.OK)
                {
                    throw new FailedException($"[Http] Lỗi yêu cầu:{apiResp.HttpStatus}=>{apiResp.Message}");
                }
                return apiResp.BusData;
            }
            catch (FailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FailedException($"[Xác thực] Lỗi khi lấy mã định danh Api:{ex.Message}");
            }
        }

        private static async Task<string> HttpPostRaw(string uri, string json, string token)
        {
            using (var req = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                req.Content = new StringContent(json ?? "", Encoding.UTF8, "application/json");
                if (!string.IsNullOrWhiteSpace(token))
                {
                    req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                using (var resp = await _http.SendAsync(req))
                {
                    return await resp.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
