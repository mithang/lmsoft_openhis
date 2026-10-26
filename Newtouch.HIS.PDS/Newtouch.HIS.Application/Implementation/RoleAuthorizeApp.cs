using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.BusinessObjects;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Application.Interface;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// Quyền vai tròApp
    /// </summary>
    public sealed class RoleAuthorizeApp : AppBase, IRoleAuthorizeApp
    {
        private readonly ISysRoleAuthorizeRepo _roleAuthorizeRepo;
        private readonly ISysModuleRepo _sysModuleRepo;

        /// <summary>
        /// Xác thực quyền truy cập
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ActionValidate(IList<string> roleIdList, string action)
        {
            if (action.StartsWith("/Home", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;    //Home/IndexĐặc biệt
            }

            if (roleIdList == null || roleIdList.Count == 0)
            {
                return false;
            }

            var opr = this.UserIdentity;
            if (string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                return false;
            }

            var authorizeurldata = GetAuthorizeActionModelList(roleIdList, opr.OrganizeId);

            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    //Nếu đã thêmHandlerAuthorizeAttribute，Thì yêu cầu có tương ứngMeum/MenuButton Và có thiết lậpUrl
                    string[] url = item.UrlAddress.Split('?');
                    if (url[0].Equals(action, System.StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Xác thực quyền truy cập (yêu cầu phải qua cách nhấp menu để mở trang）
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="moduleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ActionValidate(IList<string> roleIdList, string moduleId, string action)
        {
            if (action.StartsWith("/Home", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;    //Home/IndexĐặc biệt
            }

            if (roleIdList == null || roleIdList.Count == 0)
            {
                return false;
            }

            var opr = this.UserIdentity;
            if (string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                return false;
            }

            var authorizeurldata = GetAuthorizeActionModelList(roleIdList, opr.OrganizeId);

            authorizeurldata = authorizeurldata.FindAll(t => t.Id.Equals(moduleId, System.StringComparison.OrdinalIgnoreCase));
            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    //Nếu đã thêmHandlerAuthorizeAttribute，Thì yêu cầu có tương ứngMeum/MenuButton Và có thiết lậpUrl
                    string[] url = item.UrlAddress.Split('?');
                    if (url[0].Equals(action, System.StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #region

        /// <summary>
        /// Lấy danh sách ủy quyền
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        private List<AuthorizeActionModel> GetAuthorizeActionModelList(IList<string> roleIdList, string orgId)
        {
            var authorizeurldata = new List<AuthorizeActionModel>();

            var moduleList = _sysModuleRepo.IQueryable(t => t.zt == "1"
 && (t.OrganizeId == null || t.OrganizeId == orgId)).ToList();

            var moduleIdList = moduleList.Select(p => p.Id).ToList();

            var authorizedata = _roleAuthorizeRepo.IQueryable(t => t.zt == "1"
                && roleIdList.Contains(t.RoleId) && moduleIdList.Contains(t.ItemId)
            ).ToList();

            foreach (var item in authorizedata)
            {
                SysModuleEntity moduleEntity = moduleList.Find(t => t.Id == item.ItemId);
                if (moduleEntity != null)
                {
                    authorizeurldata.Add(new AuthorizeActionModel { Id = moduleEntity.Id, UrlAddress = moduleEntity.UrlAddress });
                }
            }

            return authorizeurldata;
        }

        #endregion

    }
}
