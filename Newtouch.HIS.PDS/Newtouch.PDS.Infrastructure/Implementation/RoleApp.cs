using NewtouchHIS.Lib.Base.Utilities;
using Newtouch.Infrastructure.EF;
using Newtouch.Tools;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// Vai tròApp
    /// </summary>
    public class RoleApp : AppBase, IRoleApp
    {
        private readonly ISysRoleRepo _roleRepository;
        private readonly ISysModuleRepo _moduleRepository;
        private readonly ISysModuleButtonRepo _moduleButtonRepository;
        private readonly ISysRoleDmnService _sysRoleDmnService;


        /// <summary>
        /// Tổ chức Danh sách vai trò mang Phân trang
        /// </summary>
        /// <param name="topOrganizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysRoleEntity> GetPagintionList(string topOrganizeId, Pagination pagination, string keyword = null)
        {
            var expression = PredicateBuilder.GetTrue<SysRoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
                expression = expression.Or(t => t.Code.Contains(keyword));
            }
            expression = expression.And(t => t.TopOrganizeId == topOrganizeId);
            expression = expression.And(t => t.zt == "1");
            return _roleRepository.FindList(expression, pagination).OrderBy(p => p.px).ToList();
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysRoleEntity GetForm(string keyValue)
        {
            return _roleRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// Xóa thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            _sysRoleDmnService.DeleteForm(keyValue);
        }

        /// <summary>
        /// Gửi và lưu
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysRoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Modify(keyValue);
            }
            else
            {
                roleEntity.Create(true);
            }
            var moduledata = _moduleRepository.IQueryable().ToList();
            var buttondata = _moduleButtonRepository.IQueryable().ToList();
            List<SysRoleAuthorizeEntity> roleAuthorizeEntityList = new List<SysRoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                SysRoleAuthorizeEntity roleAuthorizeEntity = new SysRoleAuthorizeEntity();
                roleAuthorizeEntity.Create(true);
                roleAuthorizeEntity.RoleId = roleEntity.Id;
                roleAuthorizeEntity.ItemId = itemId;
                roleAuthorizeEntity.zt = "1";   //Luôn luôn1 Hiệu lực
                if (moduledata.Find(t => t.Id == itemId) != null)
                {
                    roleAuthorizeEntity.ItemType = 1;
                }
                else if (buttondata.Find(t => t.Id == itemId) != null)
                {
                    roleAuthorizeEntity.ItemType = 2;
                }
                roleAuthorizeEntityList.Add(roleAuthorizeEntity);
            }
            _sysRoleDmnService.SubmitForm(roleEntity, roleAuthorizeEntityList, keyValue);
        }

    }
}
