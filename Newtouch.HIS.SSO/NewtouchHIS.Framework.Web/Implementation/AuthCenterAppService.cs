using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;

namespace NewtouchHIS.Framework.Web.Implementation
{
    public interface IAuthCenterAppService
    {
        /// <summary>
        /// Apptruy cậpToken
        /// </summary>
        /// <returns></returns>
        Task<string> GetAppToken(string accessAppId);
        /// <summary>
        /// người dùngTokenlấy
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> GetUserToken(OperatorModel user, string accessAppId);
        /// <summary>
        /// người dùngTokengia hạn
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> GetUserTokenRefesh(OperatorModel user, string token, string accessAppId);
    }
    public class AuthCenterAppService : IAuthCenterAppService
    {
        private readonly IHttpClientHelper _httpClient;
        /// <summary>
        /// ứng dụng cung cấp dịch vụId
        /// </summary>
        internal string? AppId;
        /// <summary>
        /// yêu cầu dịch vụAppId
        /// </summary>
        internal string? ClientAppId;
        /// <summary>
        /// ứng dụngHostđịa chỉ
        /// </summary>
        internal string? Host;
        public AuthCenterAppService(IHttpClientHelper httpClient)
        {
            _httpClient = httpClient;
            ClientAppId = ConfigInitHelper.SysConfig.AppId ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "AppId");
            Host = ConfigInitHelper.SysConfig.AppAPIHost?.SiteAuthCenterHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "SiteAuthCenterHost");
        }
        #region danh sách giao diện
        /// <summary>
        /// Apptruy cậpToken
        /// </summary>
        public string GetAppTokenApi => $"{Host}api/Auth/GetAppToken";
        /// <summary>
        /// người dùng truy cậpToken
        /// </summary>
        public string GetUserTokenApi => $"{Host}api/Auth/GetUserToken";
        /// <summary>
        /// người dùngTokengia hạn
        /// </summary>
        public string UserTokenRefreshApi => $"{Host}api/Auth/UserTokenRefresh";

        #endregion
        /// <summary>
        /// Applấy ủy quyềnToken
        /// </summary>
        /// <param name="appId">ứng dụng nàyId</param>
        /// <param name="accessAppId">xin truy cập ứng dụngId</param>
        /// <returns></returns>
        public async Task<string> GetAppToken(string accessAppId)
        {
            var reqObj = new
            {
                AppId = ClientAppId,
                Data = accessAppId
            };
            return await HttpPostWithTokenAsync(reqObj.ToJson(), GetAppTokenApi);
        }

        /// <summary>
        /// người dùng lấyToken
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserToken(OperatorModel user, string accessAppId)
        {
            var reqObj = new
            {
                AppId = ClientAppId,
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
            return await HttpPostWithTokenAsync(reqObj.ToJson(), GetUserTokenApi, null);
        }
        /// <summary>
        /// người dùngTokengia hạn
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> GetUserTokenRefesh(OperatorModel user, string token, string accessAppId)
        {
            var reqObj = new
            {
                AppId = ClientAppId,
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
            return await HttpPostWithTokenAsync(reqObj.ToJson(), UserTokenRefreshApi, token);
        }

        private async Task<string> HttpPostWithTokenAsync(string request, string method, string? token = null)
        {
            Result tokenResult;
            if(!string.IsNullOrWhiteSpace(token))
            {
                tokenResult = await _httpClient.PostAsync<Result>(method, request, token);
            }
            else
            {
                tokenResult = await _httpClient.PostAsync<Result>(method, request);
            }
            
            if (tokenResult != null && tokenResult.BusData != null)
            {
                var tokenObj = JsonConvert.DeserializeObject<BusResult<JWTToken>>(tokenResult.BusData.ToJson());
                if (tokenObj != null && tokenObj.code==ResponseResultCode.SUCCESS && tokenObj.Data!=null)
                {
                    return tokenObj.Data.Token;
                }
                else if(tokenObj != null && tokenObj.code==ResponseResultCode.FAIL)
                {
                    throw new FailedException($"{tokenObj.msg}");
                }
                else
                {
                    throw new FailedException($"Path:({method}):{tokenObj?.msg}");
                }
            }
            
            throw new FailedException($"[鉴权]接口返回异常({method}):{tokenResult?.ToJson()}");
        }
    }
}
