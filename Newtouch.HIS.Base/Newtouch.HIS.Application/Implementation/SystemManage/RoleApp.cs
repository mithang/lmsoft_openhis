using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Core.Common;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Application.Implementation.SystemManage
{
    /// <summary>
    /// App vai trò
    /// </summary>
    public class RoleApp : AppBase, IRoleApp
    {
        private readonly ISysRoleRepo _roleRepository;
        private readonly ISysModuleRepo _moduleRepository;
        private readonly ISysModuleButtonRepo _moduleButtonRepository;
        private readonly ISysRoleDmnService _sysRoleDmnService;

        public RoleApp(ISysRoleRepo roleRepository, ISysModuleRepo moduleRepository,
            ISysModuleButtonRepo moduleButtonRepository, ISysRoleDmnService sysRoleDmnService)
        {
            this._roleRepository = roleRepository;
            this._moduleRepository = moduleRepository;
            this._moduleButtonRepository = moduleButtonRepository;
            this._sysRoleDmnService = sysRoleDmnService;
        }

        /// <summary>
        /// Danh sách vai trò của tổ chức, có phân trang
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysRoleEntity> GetPagintionList(string orgId, Pagination pagination, string keyword = null)
        {
            var expression = ExtLinq.True<SysRoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
                expression = expression.Or(t => t.Code.Contains(keyword));
            }
            //||IsAdministrator Việc sửa quyền của vai trò mà mình thuộc về có phần không phù hợp
            if (orgId == Constants.TopOrganizeId && (this.UserIdentity.IsRoot || this.UserIdentity.IsAdministrator))
            {
                expression = expression.And(t => t.OrganizeId == orgId);
                expression = expression.Or(t => t.OrganizeId == "*");  //Kèm theo quản trị viên hệ thống Administrator
            }
            else
            {
                expression = expression.And(t => t.OrganizeId == orgId);
            }
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
        /// Gửi để lưu
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysRoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            //Chủ yếu để đảm bảo khóa chính duy nhất
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.Modify(keyValue);
            }
            else
            {
                roleEntity.Create(true);
            }

            List<SysRoleAuthorizeEntity> roleAuthorizeEntityList = new List<SysRoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                SysRoleAuthorizeEntity roleAuthorizeEntity = new SysRoleAuthorizeEntity();
                roleAuthorizeEntity.Create(true);
                roleAuthorizeEntity.RoleId = roleEntity.Id;
                roleAuthorizeEntity.ItemId = itemId;
                roleAuthorizeEntity.zt = "1";   //Luôn bằng 1 mới có hiệu lực
                roleAuthorizeEntityList.Add(roleAuthorizeEntity);
            }
            _sysRoleDmnService.SubmitForm(roleEntity, roleAuthorizeEntityList, keyValue);
        }

    }
}
