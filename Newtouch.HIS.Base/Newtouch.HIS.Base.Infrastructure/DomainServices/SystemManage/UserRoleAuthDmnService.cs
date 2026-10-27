using Newtouch.HIS.Domain;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Quyền vai trò người dùng Dùng chungDmnService
    /// </summary>
    public class UserRoleAuthDmnService : DmnServiceBase, IUserRoleAuthDmnService
    {
        public UserRoleAuthDmnService()
            
        {
        }

        /// <summary>
        /// Lấy danh sách vai trò người dùng đã được ủy quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetUserRoleList(string userId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(orgId))
            {
                return new List<SysRoleEntity>();
            }

            var sql = @"select distinct b.* from Sys_UserRole(nolock) a
left join Sys_Role(nolock) b
on a.RoleId = b.Id
where 1 = 1
and b.OrganizeId = @orgId
and a.UserId = @userId
and a.zt = '1' and b.zt = '1'";

            return this.FindList<SysRoleEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@userId", userId)
                    ,new SqlParameter("@orgId", orgId)
                });
        }

        /// <summary>
        /// Lấy vai trò Liên kết Người dùng（org）
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<FirstSecond> GetCurUserIdListByRoleId(string roleId)
        {
            var sql = @"select distinct UserId First, b.OrganizeId Second 
                from Sys_UserRole(nolock) a
                left join Sys_Role(nolock) b
                on a.RoleId = b.Id
                where a.zt = '1'
                and b.Id = @roleId";
            return this.FindList<FirstSecond>(sql, new SqlParameter[] {
                new SqlParameter("@roleId",roleId)
            });
        }

        /// <summary>
        /// Lưu vai trò Danh sách người dùng
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        public void submitRoleUser(string roleId, List<FirstSecond> userList)
        {
            using (var db = new DbTrans())
            {
                var roleEntity = db.IQueryable<SysRoleEntity>(p => p.Id == roleId).FirstOrDefault();

                if (userList != null)
                {
                    foreach (var user in userList)
                    {
                        if (user.Second != roleEntity.OrganizeId)
                        {
                            throw new FailedException("Gửi sai, người dùng và vai trò không tương ứng cơ sở");
                        }
                    }
                }

                //Trước del
                db.Delete<SysUserRoleEntity>(p => p.RoleId == roleId);

                //Thêm tiếp
                if (userList != null)
                {
                    foreach (var user in userList)
                    {
                        var userRoleEntity = new SysUserRoleEntity();
                        userRoleEntity.Id = Guid.NewGuid().ToString();
                        userRoleEntity.RoleId = roleId;
                        userRoleEntity.UserId = user.First;
                        userRoleEntity.Create();
                        db.Insert(userRoleEntity);
                    }
                }
                db.Commit();
            }
        }

        /// <summary>
        /// Cập nhật vai trò người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <param name="roleIdList"></param>
        public void UpdateUserRole(string userId, string orgId, string[] roleIdList)
        {
            //vai tròlist
            var roleLists = new List<SysUserRoleEntity>();
            foreach (var item in roleIdList.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct())
            {
                var entity = new SysUserRoleEntity();
                entity.Create(true);
                entity.UserId = userId;
                entity.RoleId = item;
                entity.zt = "1";
                roleLists.Add(entity);
            }

            using (var db = new DbTrans())
            {
                //Vai trò tổ chứcIdDanh sách
                var orgRoleIdList = db.IQueryable<SysRoleEntity>(p => p.OrganizeId == orgId).Select(p => p.Id).ToList();
                //
                var oldRoleList = db.IQueryable<SysUserRoleEntity>().Where(p => p.UserId == userId
                    && orgRoleIdList.Contains(p.RoleId)).ToList();
                for (int i = 0; i < roleLists.Count; i++)
                {
                    //Hạn chế：Liên kết vai trò của tổ chức khác

                    if (oldRoleList.Any(p => p.RoleId == roleLists[i].RoleId))
                    {
                        oldRoleList.Remove(oldRoleList.Where(p => p.RoleId == roleLists[i].RoleId).First());
                        continue;
                    }
                    db.Insert(roleLists[i]);
                }
                foreach (var item in oldRoleList)
                {
                    db.Delete(item);
                }
                db.Commit();
            }
        }

    }
}
