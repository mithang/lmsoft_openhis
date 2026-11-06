using Newtouch.HIS.Domain.IDomainServices;
using System.Collections.Generic;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using System.Linq;
using NewtouchHIS.Lib.Base.Exceptions;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// vai trò DmnService
    /// </summary>
    public class SysRoleDmnService : DmnServiceBase, ISysRoleDmnService
    {
        public SysRoleDmnService()
            
        {
        }

        /// <summary>
        /// Xóa vai trò hệ thống
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            using (var db = new DbTrans())
            {
                var dbRoleEntity = db.IQueryable<SysRoleEntity>(p => p.Id == keyValue).FirstOrDefault();
                // NX20-003 (D-NX20-003): FirstOrDefault tra null khi keyValue
                // khong ton tai — deref .Code cu gay NRE; chan bang loi
                // nghiep vu theo mau NX18-001 (Center SysRoleDmnService).
                if (dbRoleEntity == null)
                {
                    throw new FailedException("Không tìm thấy vai trò cần xóa");
                }
                if (dbRoleEntity.Code == "Administrator")
                {
                    throw new FailedException("MãAdministratorđược hệ thống giữ lại, không thể xóa");
                }

                db.Delete<SysRoleEntity>(t => t.Id == keyValue);
                db.Delete<SysRoleAuthorizeEntity>(t => t.RoleId == keyValue);
                db.Commit();
            }
        }

        /// <summary>
        /// Gửi tạo mới,Cập nhật Thực thể
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        /// <param name="isAdministrator"></param>
        public void SubmitForm(SysRoleEntity roleEntity, List<SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue)
        {
            if (roleEntity.Code == "Administrator")
            {
                if (string.IsNullOrWhiteSpace(keyValue)
                    || (!string.IsNullOrEmpty(keyValue) && roleEntity.OrganizeId != "*"))
                {
                    throw new FailedException("MãAdministratorđược hệ thống giữ lại");
                }
            }

            using (var db = new DbTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var dbRoleEntity = db.IQueryable<SysRoleEntity>(p => p.Id == keyValue).FirstOrDefault();
                    // NX20-003 (D-NX20-003): nhu DeleteForm tren — chan
                    // NRE bang loi nghiep vu theo mau NX18-001.
                    if (dbRoleEntity == null)
                    {
                        throw new FailedException("Không tìm thấy vai trò cần sửa");
                    }

                    dbRoleEntity.Name = roleEntity.Name;
                    dbRoleEntity.Code = roleEntity.Code;
                    dbRoleEntity.px = roleEntity.px;
                    dbRoleEntity.zt = roleEntity.zt;
                    dbRoleEntity.Description = roleEntity.Description;

                    dbRoleEntity.Modify();
                    db.Update(dbRoleEntity);
                }
                else
                {
                    //roleEntity.Create(true);
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
