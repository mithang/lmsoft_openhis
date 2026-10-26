using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;

namespace NewtouchHIS.Framework.Web.Controllers
{
    /// <summary>
    /// nhiều tổ chức Controllerlớp cơ sở
    /// </summary>
    public abstract class OrgControllerBase : BaseController
    {
        protected OrgControllerBase()
        {
        }


        /// <summary>
        /// tổ chứcId
        /// </summary>
        protected string? OrganizeId
        {
            get
            {
                if (this.UserIdentity == null)
                {
                    return null;
                }
                return this.UserIdentity.OrganizeId;
            }
        }


        /// <summary>
        /// lấy quyềnOrg
        /// </summary>
        /// <param name="throwFailedException"></param>
        /// <returns></returns>
        [NonAction]
        protected string? GetAuthOrganizeId(bool throwFailedException = true)
        {
            if (UserIdentity == null)
            {
                return null;
            }
            string? orgId;
            if (UserIdentity.IsAdministrator || UserIdentity.IsRoot)
            {
                //nếu là quản trị viên hệ thống thì để nó lấy dữ liệu của tất cả các tổ chức
                orgId = ConfigInitHelper.SysConfig?.Top_OrganizeId;
            }
            else
            {
                //mặc định dùng ... của người dùng liên kết hiện tạiOrganizeId
                orgId = UserIdentity.OrganizeId;
            }
            if (throwFailedException && string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("定位当前权限内的组织机构失败");
            }
            return orgId;
        }

        #region token
        protected string GetUserCacheKey => SystemKey.AssemblyUserTokenKey(UserIdentity.UserCode, OrganizeId);




        #endregion


    }
}
