using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Exceptions;


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
                // G37 F37-8: role phai ton tai — keyValue bogus -> loi TV
                // thay vi Success 0 dong (mau guard NX20-003).
                var dbRoleEntity = db.IQueryable<SysRoleEntity>(p => p.Id == keyValue).FirstOrDefault();
                if (dbRoleEntity == null)
                {
                    throw new FailedException("Không tìm thấy vai trò cần xóa");
                }
                db.Delete<SysRoleEntity>(t => t.Id == keyValue);
                db.Delete<SysRoleAuthorizeEntity>(t => t.RoleId == keyValue);
                // G37 F37-8: xoa ca Sys_UserRole trong cung transaction —
                // neu khong, user van giu RoleId tro toi role da xoa
                // (dong mo coi).
                db.Delete<SysUserRoleEntity>(t => t.RoleId == keyValue);
                db.Commit();
            }
        }

        /// <summary>
        /// Gửi mới,Cập nhật Thực thể
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
                    // NX20-003 (D-NX20-003): db.Update im lang khi Id khong
                    // ton tai (0 row) roi van xoa + chen lai authorize va
                    // commit success — chan bang loi nghiep vu theo mau
                    // NX18-001. Chu y: EFDbTransaction.FirstOrDefault/
                    // FindEntity dung .First() (nem khi rong) nen guard
                    // phai dung IQueryable(...).FirstOrDefault() (LINQ
                    // tren list trong bo nho, tra null khi thieu).
                    var dbRoleEntity = db.IQueryable<SysRoleEntity>(p => p.Id == keyValue).FirstOrDefault();
                    if (dbRoleEntity == null)
                    {
                        throw new FailedException("Không tìm thấy vai trò cần sửa");
                    }
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
