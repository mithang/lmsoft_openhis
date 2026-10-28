using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;

namespace NewtouchHIS.Framework.Web.Implementation
{
    public interface ILoginAppService : IScopedDependency
    {
        /// <summary>
        /// kiểm tra đăng nhập
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<SysUserVEntity> CheckPwdAsync(LoginBO request);
        /// <summary>
        /// khởi tạo dữ liệu người dùng
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<OperatorModel> BuildLoginStatusOprAsync(string userId, OperatorModel user);
    }
    public class LoginAppService : AppServiceBase, ILoginAppService
    {
        public LoginAppService(IHttpClientHelper httpClient, IAuthCenterAppService authCenterApp) : base(httpClient, authCenterApp)
        {
            ClientAppId = ConfigInitHelper.SysConfig.AppId ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "AppId");
            AppId = ConfigInitHelper.SysConfig.AppAPIHostName?.HisAppBaseAPIHost ?? "HIS.BaseAPI";
            Host = ConfigInitHelper.SysConfig.AppAPIHost?.HisAppBaseAPIHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "HisAppBaseAPIHost");

        }
        #region API danh sách
        /// <summary>
        /// kiểm tra đăng nhập
        /// </summary>
        public string CheckPwdApi => $"{Host}api/login/CheckPwd";
        public string BuildLoginStatusOprApi => $"{Host}api/login/BuildLoginStatusOpr";

        #endregion
        public async Task<SysUserVEntity> CheckPwdAsync(LoginBO request)
        {
            var result = await HttpPostAnonymous<SysUserVEntity>(RequestAsync(request).ToJson(), CheckPwdApi);
            if (result != null && result.code == ResponseResultCode.SUCCESS && result.Data != null)
            {
                return result.Data;
            }
            else if (result != null && result.code == ResponseResultCode.FAIL)
            {
                // QA-phase2 (SETT-16): the login page shows the exception
                // message verbatim, and an upstream built from older source
                // returns the login-failure text in Chinese ("登录失败，请联系管理员").
                // Map that known message to Vietnamese here so the user never
                // sees it, whatever the upstream build. Unknown messages pass
                // through unchanged for diagnosis.
                var upstreamMsg = result?.msg ?? "";
                upstreamMsg = upstreamMsg.Replace("登录失败，请联系管理员", "Đăng nhập thất bại, vui lòng liên hệ quản trị viên");
                throw new FailedException($"Đăng nhập thất bại, vui lòng liên hệ quản trị viên:{upstreamMsg}");
            }
            throw new FailedException($"Đăng nhập thất bại, vui lòng liên hệ quản trị viên");
        }
        public async Task<OperatorModel> BuildLoginStatusOprAsync(string userId, OperatorModel user)
        {
            if (string.IsNullOrWhiteSpace(user.OrganizeId))
            {
                user.OrganizeId = user.TopOrganizeId;
            }
            return await HttpPostWithToken<OperatorModel, string>(RequestAsync(userId), BuildLoginStatusOprApi, user);
        }
    }
}
