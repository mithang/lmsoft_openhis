using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;


namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Vai trò DmnService
    /// </summary>
    public class SysRoleDmnService : DmnServiceBase, ISysRoleDmnService
    {
        public SysRoleDmnService(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }

        /// <summary>
        /// Xóa vai trò hệ thống
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                db.Delete<SysRoleEntity>(t => t.Id == keyValue);
                db.Delete<SysRoleAuthorizeEntity>(t => t.RoleId == keyValue);
                db.Commit();
            }
        }

        /// <summary>
        /// Gửi mới、Cập nhật Thực thể
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysRoleEntity roleEntity, List<SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    List<string> igone = new List<string>();
                    db.Update(roleEntity);
                }
                else
                {
                    db.Insert(roleEntity);
                }

                db.Delete<SysRoleAuthorizeEntity>(t => t.RoleId == roleEntity.Id);

                for (int i = 0; i < roleAuthorizeEntitys.Count; i++)
                {
                    db.Insert(roleAuthorizeEntitys[i]);
                }
                db.Commit();
            }
        }

    }
}
