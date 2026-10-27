using Microsoft.AspNetCore.Http;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.Authentication;
using System.Security.Claims;

namespace NewtouchHIS.Lib.Services
{
    /// <summary>
    /// Dựa trênClaimsPrincipalgiao diện thông tin người dùng được triển khai.
    /// </summary>
    [Serializable]
    public class IdentityCachePrincipal : IIdentityCache
    {
        /// <summary>
        /// IHttpContextAccessorĐối tượng
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// NếuIHttpContextAccessor.HttpContext?.Userkhông rỗng thì lấyHttpContextcủaClaimsPrincipal,nếu không thì lấy của luồngCurrentPrincipal
        /// </summary>
        protected ClaimsPrincipal Principal => _httpContextAccessor!.HttpContext!.User ?? (Thread.CurrentPrincipal as ClaimsPrincipal);

        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public IdentityCachePrincipal(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserIdentity identity => new UserIdentity
        {
            TopOrganizeId = this.Principal!.FindFirst(IdentityClaimTypes.TopOrganizeId)!.Value,
            OrganizeId = this.Principal!.FindFirst(IdentityClaimTypes.OrganizeId)!.Value,
            UserId = this.Principal!.FindFirst(IdentityClaimTypes.UserId)!.Value,
            UserCode = this.Principal!.FindFirst(IdentityClaimTypes.UserCode)!.Value,
            UserName = this.Principal!.FindFirst(IdentityClaimTypes.UserName)!.Value,
            AppId = this.Principal!.FindFirst(IdentityClaimTypes.AppId)!.Value,
            Account = this.Principal!.FindFirst(IdentityClaimTypes.Account)!.Value
        };
        public void SetIdentityInfo(UserIdentity info, string appId)
        {
             
        }
    }
}
