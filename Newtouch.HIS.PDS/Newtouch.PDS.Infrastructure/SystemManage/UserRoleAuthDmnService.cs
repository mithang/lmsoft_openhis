using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
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
        /// LấyUserId TheoRoleId
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
            using (var db = new EFDbTransaction().BeginTrans())
            {
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
                //
                db.Delete<SysUserRoleEntity>(p => p.RoleId == roleId && hospUserIdList.Contains(p.UserId));

                //Thêm nữa
                var userIdArr = (userIds ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
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
