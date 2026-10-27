using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.Authentication;
using NewtouchHIS.Lib.Base.Utilities;
using System.DirectoryServices.ActiveDirectory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace NewtouchHIS.Lib.Services.Authentication
{

    /// <summary>
    /// Mã hóa thuận nghịch đối xứng
    /// </summary>
    public class HSJWTService : IJWTService
    {
        private readonly JWTOptions _jWTOptions;
        private string _MsgPrefix = "[Tin nhắn xác thực]:";
        public HSJWTService(IOptionsMonitor<JWTOptions> jwtOptions)
        {
            _jWTOptions = jwtOptions.CurrentValue;
        }

        public async Task<BusResult<JWTToken>> GetTokenAsync(JWTTokenRequest request)
        {
            string jwtToken = "";
            var identity = request.authIdentity;

            OptionValid(request);
            string cacheKey = GetCacheKey(request);
            string findCacheKey = RedisHelper.GetString(cacheKey) ?? "";
            if (!string.IsNullOrWhiteSpace(findCacheKey))
            {
                return new BusResult<JWTToken> { code = ResponseResultCode.SUCCESS, Data = new JWTToken { Token = findCacheKey } };
            }
            await Task.Run(() =>
            {

                var expTime = DateTime.Now.AddHours(_jWTOptions.ExpireHours);
                var claims = new List<Claim>
                {
                    new Claim(IdentityClaimTypes.AppId, identity.AppId),
                    new Claim(IdentityClaimTypes.OrganizeId, identity.OrganizeId??""),
                    new Claim(IdentityClaimTypes.TopOrganizeId, identity.TopOrganizeId ?? ""),
                    new Claim(IdentityClaimTypes.Account, identity.Account??""),
                    new Claim(IdentityClaimTypes.UserCode, identity.UserCode??""),
                    new Claim(IdentityClaimTypes.UserName, identity.UserName??""),
                    new Claim(IdentityClaimTypes.UserId, identity.UserId??""),
                    new Claim(IdentityClaimTypes.TokenType, identity.TokenType??""),
                    new Claim(IdentityClaimTypes.Token, identity.Token??""),
                };
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jWTOptions.Key));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _jWTOptions.Issuer,
                    audience: _jWTOptions.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(_jWTOptions.ExpireHours),
                    signingCredentials: creds);
                jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                RedisHelper.SetString(cacheKey, jwtToken,(HourOfDay)_jWTOptions.ExpireHours);
            });

            return new BusResult<JWTToken> { code = ResponseResultCode.SUCCESS, Data = new JWTToken { Token = jwtToken } };
        }

        public async Task TokenRefresh(JWTTokenRequest request)
        {
            var key = RedisHelper.GetString(GetCacheKey(request));

        }

        #region private
        private void OptionValid(JWTTokenRequest request)
        {
            if (request == null || request.authIdentity == null)
            {
                throw new FailedException($"{_MsgPrefix}Vui lòng bổ sung thông tin danh tính");
            }
            var isAuth = ConfigInitHelper.SysConfig.AuthAppIds!.FirstOrDefault(p => p == (request.authIdentity.AppId));
            if (isAuth == null)
            {
                throw new FailedException($"{_MsgPrefix}AppId chưa được ủy quyền");
            }
            switch (request.authType)
            {
                case AuthType.Web:
                    if (string.IsNullOrWhiteSpace(request.authIdentity.Account))
                    {
                        throw new FailedException($"{_MsgPrefix}Thông tin tài khoản không được để trống");
                    }
                    if (string.IsNullOrWhiteSpace(request.authIdentity.TopOrganizeId) || string.IsNullOrWhiteSpace(request.authIdentity.OrganizeId))
                    {
                        throw new FailedException($"{_MsgPrefix}Thông tin tổ chức không được để trống");
                    }
                    break;
                case AuthType.WebApi:
                    break;
                default:
                    throw new FailedException($"{_MsgPrefix}Không hỗ trợ phương thức ủy quyền này");
                    //break;
            }
        }

        private string GetCacheKey(JWTTokenRequest request)
        {
            return $"AuthToken{request.authType}_{request.authIdentity!.AppId}_{request.authIdentity.OrganizeId ?? "Org*".Replace("-", "")}_{request.authIdentity!.Account ?? "App"}";
        }
        #endregion
    }
}
