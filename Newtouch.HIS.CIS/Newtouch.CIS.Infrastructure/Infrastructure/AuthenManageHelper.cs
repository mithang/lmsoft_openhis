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
        //用户登陆缓存token
        public static string userCacheToken = "";

        /// <summary>
        /// 获取中心接口api token
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
        /// Post 请求
        /// 鉴权失败自动刷新
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
                    throw new FailedException("[鉴权]未获取到 Api 身份令牌，请联系管理员");
                }
                string response = HttpMethods.HttpPostWithToken(uri, data, token);
                ApiManageResponse<T> apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                if (apiResp.HttpStatus == (int)HttpStatusCode.Unauthorized)
                {
                    //鉴权失败 刷新token
                    token = GetUserTokenRefesh(user, token);
                    //重新请求接口
                    response = HttpMethods.HttpPostWithToken(uri, data, token);
                    apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                }
                if (apiResp.HttpStatus != (int)HttpStatusCode.OK)
                {
                    throw new FailedException($"[Http]请求异常：{apiResp.HttpStatus}=>{apiResp.Message}");
                }
                return apiResp.BusData;
            }
            catch (Exception ex)
            {
                throw new FailedException($"请求异常：{ex.Message}");
            }
        }

        /// <summary>
        /// token 过期 强制刷新
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
        /// 用户登陆后获取api通用 token
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
                throw new FailedException("[鉴权]未读取到接口AppId配置（Token_ApiManage）信息");
            }
            if (string.IsNullOrWhiteSpace(userCacheToken))
            {
                userCacheToken = string.Format(CacheKey.AccessToken, appId, orgId, accessAppId) + $"_{user}";
            }
            return userCacheToken;
        }

        #region API
        /// <summary>
        /// 批量处理已读状态
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
