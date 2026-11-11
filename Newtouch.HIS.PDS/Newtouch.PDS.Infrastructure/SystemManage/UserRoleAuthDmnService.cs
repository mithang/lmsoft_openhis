using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using NewtouchHIS.Lib.Base.Exceptions;
using System;
using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;


namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Quyền vai trò người dùng Dùng chungDmnService
    /// </summary>
    public class UserRoleAuthDmnService : DmnServiceBase, IUserRoleAuthDmnService
    {
        public UserRoleAuthDmnService(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy danh sách vai trò người dùng đã ủy quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetUserRoleList(string userId)
        {
            var sql = @"select distinct c.* from Sys_UserRole a
left join [NewtouchHIS_Base].[dbo].V_C_Sys_User b
on a.UserId = b.Id
left join Sys_Role c
on a.RoleId = c.Id
where a.UserId = @userId and a.zt = '1' and c.zt = '1'";

            return this.FindList<SysRoleEntity>(sql, new SugarParameter[] {
                new SugarParameter("@userId", userId ?? "")
            });
        }

        /// <summary>
        /// Lấy UserId TheoRoleId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<string> GetCurUserIdListByRoleId(string roleId)
        {
            var sql = "select distinct UserId from [dbo].[Sys_UserRole] where zt = '1' and RoleId = @roleId";
            return this.FindList<string>(sql, new SugarParameter[] {
                new SugarParameter("@roleId",roleId)
            });
        }

        /// <summary>
        /// Lưu Vai trò Danh sách người dùng
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        public void submitRoleUser(string roleId, string userIds, string parentOrgId)
        {
            // G37 F37-9: roleId rong -> loi TV thay vi INSERT RoleId NULL
            // (loi SQL tho, 10d).
            if (string.IsNullOrWhiteSpace(roleId))
            {
                throw new FailedException("Không tìm thấy vai trò");
            }
            using (var db = new EFDbTransaction().BeginTrans())
            {
                // G37 F37-9: role phai ton tai — roleId bogus -> loi TV
                // thay vi INSERT dong Sys_UserRole mo coi (10c). Guard mau
                // NX20-003: IQueryable(...).FirstOrDefault(), khong dung
                // FirstOrDefault truc tiep (nem khi rong).
                var dbRoleEntity = db.IQueryable<SysRoleEntity>(p => p.Id == roleId).FirstOrDefault();
                if (dbRoleEntity == null)
                {
                    throw new FailedException("Không tìm thấy vai trò");
                }
                //trước del
                var hospUserIdList = this.FindList<string>(@"
WITH cteTree
    AS (SELECT *
            FROM [NewtouchHIS_Base]..V_S_Sys_Organize
            WHERE Id = @parentOrgId --Truy vấn đầu tiên làm điểm cơ sở đệ quy(Điểm neo)
        UNION ALL
        SELECT [NewtouchHIS_Base]..V_S_Sys_Organize.*     --Truy vấn thứ hai làm thành viên đệ quy， Khi kết quả thành viên cấp dưới trống，Đệ quy này kết thúc。
            FROM
                cteTree INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Organize ON cteTree.Id= [NewtouchHIS_Base]..V_S_Sys_Organize.ParentId) 


select distinct a.UserId from [NewtouchHIS_Base]..V_S_Sys_UserStaff(nolock) a
left join  [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) b
on a.StaffId = b.Id
where b.OrganizeId  in (select Id from cteTree)"
, new[] { new SugarParameter("@parentOrgId", parentOrgId) });
                // G37 F37-9: validate user TRUOC khi DELETE — user la /
                // khong thuoc cay org hien tai -> loi TV, khong xoa user cu
                // cua role (10b: user bogus tao dong mo coi).
                var userIdArr = (userIds ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
                var invalidUserIds = userIdArr.Where(id => hospUserIdList == null || !hospUserIdList.Contains(id)).ToList();
                if (invalidUserIds.Count > 0)
                {
                    throw new FailedException("Người dùng không tồn tại hoặc không thuộc tổ chức hiện tại: " + string.Join(",", invalidUserIds));
                }
                //
                db.Delete<SysUserRoleEntity>(p => p.RoleId == roleId && hospUserIdList.Contains(p.UserId));

                //Thêm nữa
                foreach (var userId in userIdArr)
                {
                    var userRoleEntity = new SysUserRoleEntity();
                    userRoleEntity.Id = Guid.NewGuid().ToString();
                    userRoleEntity.RoleId = roleId;
                    userRoleEntity.UserId = userId;
                    userRoleEntity.Create();
                    db.Insert(userRoleEntity);
                }
                db.Commit();
            }
        }


    }
}
