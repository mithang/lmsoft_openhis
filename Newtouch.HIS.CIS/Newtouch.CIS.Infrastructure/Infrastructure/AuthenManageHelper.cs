using Newtonsoft.Json;
using Newtouch.CIS.Compat;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using System;
using System.Net;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Ported from Framework CIS AuthenManageHelper. web.config AppSettings
    /// reads are replaced by AppConfig (appsettings.json, set at startup);
    /// Redis / HTTP / exception helpers come from Newtouch.CIS.Compat.
    /// </summary>
    public class AuthenManageHelper
    {
        public static string SiteTokenAPIHost => AppConfig.SiteTokenAPIHost;
        public static string SiteConmonAPIHost => AppConfig.SiteConmonAPIHost;
        public static string appId => AppConfig.AppId;
        public static string accessAppId => AppConfig.Token_ApiManage;
        //Người dùngđănglụcchậmtồn tạitoken
        public static string userCacheToken = "";

        /// <summary>
        /// Lấytrongtimgiao diệnapi token
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            string tokenstr = RedisHelper.StringGet("AuthenGetTokenKey");
            string tokenstrs = "";
            if (!string.IsNullOrWhiteSpace(tokenstr))
            {
                tokenstrs = tokenstr;
            }
            else
            {
                var reqObj = new
                {
                    AppId = appId,
                    Data = accessAppId
                };
                tokenstrs = HttpMethods.HttpPost(SiteTokenAPIHost + "api/Auth/GetAppToken", reqObj.ToJson(), null);
                var Token = JsonConvert.DeserializeObject<ApiManageResponse<Tokendata>>(tokenstrs);
                var data = Token.BusData.data;
                RedisHelper.StringSet("AuthenGetTokenKey", data.Token, new TimeSpan(0, 20, 0));
                tokenstrs = data.Token;
            }
            return tokenstrs;
        }

        public static string HttpPost(string data, string uri, string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                return HttpMethods.HttpPostWithToken(uri, data, token);
            }
            return HttpMethods.HttpPost(uri, data, null);
        }

        /// <summary>
        /// Post yêu cầu
        /// Xác thựcthất bạitựđộngLàm mới
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="uri"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static BusData<T> HttpPost<T>(string data, string uri, OperatorModel user)
        {
            string token = "";
            try
            {
                token = GetUserToken(user);
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new FailedException("[Xác thực]chưaLấyđến Api thânphầnmã thông báo，vui lòngliênhệQuản lýviên");
                }
                string response = HttpMethods.HttpPostWithToken(uri, data, token);
                ApiManageResponse<T> apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                if (apiResp.HttpStatus == (int)HttpStatusCode.Unauthorized)
                {
                    //Xác thựcthất bại Làm mớitoken
                    token = GetUserTokenRefesh(user, token);
                    //lạiyêu cầugiao diện
                    response = HttpMethods.HttpPostWithToken(uri, data, token);
                    apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                }
                if (apiResp.HttpStatus != (int)HttpStatusCode.OK)
                {
                    throw new FailedException($"[Http]yêu cầuNgoại lệ：{apiResp.HttpStatus}=>{apiResp.Message}");
                }
                return apiResp.BusData;
            }
            catch (Exception ex)
            {
                throw new FailedException($"yêu cầuNgoại lệ：{ex.Message}");
            }
        }

        /// <summary>
        /// token quakỳ mạnhchếLàm mới
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetUserTokenRefesh(OperatorModel user, string token)
        {
            var key = GetUserCacheKey(user.OrganizeId, user.UserCode);
            var reqObj = new
            {
                AppId = appId,
                OrganizeId = user.OrganizeId,
                Data = new
                {
                    TopOrganizeId = user.TopOrganizeId,
                    UserCode = user.UserCode,
                    UserName = user.UserName,
                    Account = user.UserCode,
                    Timestamp = DateTime.Now,
                    KeyLicense = accessAppId
                }
            };
            string tokenstrs = HttpMethods.HttpPostWithToken(SiteTokenAPIHost + "api/Auth/UserTokenRefresh", reqObj.ToJson(), token);
            var Token = JsonConvert.DeserializeObject<ApiManageResponse<Tokendata>>(tokenstrs);
            RedisHelper.StringSet(key, Token.BusData.data.Token, new TimeSpan(0, 20, 0));
            return Token.BusData.data.Token;
        }

        /// <summary>
        /// Người dùngđănglụcsauLấyapiChung token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetUserToken(OperatorModel user)
        {
            var key = GetUserCacheKey(user.OrganizeId, user.UserCode);
            string tokenstr = RedisHelper.StringGet(key);
            string tokenstrs = "";
            if (!string.IsNullOrWhiteSpace(tokenstr))
            {
                tokenstrs = tokenstr;
            }
            else
            {
                var reqObj = new
                {
                    AppId = appId,
                    OrganizeId = user.OrganizeId,
                    Data = new
                    {
                        TopOrganizeId = user.TopOrganizeId,
                        UserCode = user.UserCode,
                        UserName = user.UserName,
                        Account = user.UserCode,
                        KeyLicense = accessAppId
                    },
                    Timestamp = DateTime.Now,
                };
                tokenstrs = HttpMethods.HttpPost(SiteTokenAPIHost + "api/Auth/GetUserToken", reqObj.ToJson(), null);
                var Token = JsonConvert.DeserializeObject<ApiManageResponse<Tokendata>>(tokenstrs);
                RedisHelper.StringSet(key, Token.BusData.data.Token, new TimeSpan(0, 20, 0));
                tokenstrs = Token.BusData.data.Token;
            }
            return tokenstrs;
        }

        public static string GetUserCacheKey(string orgId, string user = null)
        {
            if (string.IsNullOrWhiteSpace(accessAppId))
            {
                throw new FailedException("[Xác thực]chưađọclấyđếngiao diệnAppIdcấu hình（Token_ApiManage）thông tin");
            }
            if (string.IsNullOrWhiteSpace(userCacheToken))
            {
                userCacheToken = string.Format(CacheKey.AccessToken, appId, orgId, accessAppId) + $"_{user}";
            }
            return userCacheToken;
        }

        #region API
        /// <summary>
        /// lôlượngchỗlýđãđọcTrạng thái
        /// </summary>
        public static string NoticeReadSendApi => $"{SiteConmonAPIHost}api/NoticeJob/NoticeListRead";
        #endregion
    }

    #region ResponseDTO
    public class AuthenTokenHelper
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData BusData { get; set; }
    }

    public class BusData
    {
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }

    public class Tokendata
    {
        public string Token { get; set; }
    }

    public class ApiManageRequest<T>
    {
        public string AppId { get; set; }
        public string OrganizeId { get; set; }
        public string Timestamp
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        public T Data { get; set; }
    }
    #endregion
}
