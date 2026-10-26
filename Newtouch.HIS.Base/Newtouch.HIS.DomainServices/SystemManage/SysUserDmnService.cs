using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Security;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// DmnService liên quan đến người dùng
    /// </summary>
    public class SysUserDmnService : DmnServiceBase, ISysUserDmnService
    {
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserLogOnRepository _sysUserLogOnRepository;
        private readonly ISysRoleRepo _sysRoleRepository;
        private readonly ISysUserRoleRepo _sysUserRoleRepository;

        public SysUserDmnService(IBaseDatabaseFactory databaseFactory
            , ISysUserRepo sysUserRepo, ISysUserLogOnRepository sysUserLogOnRepository
            , ISysRoleRepo sysRoleRepository, ISysUserRoleRepo sysUserRoleRepository)
            : base(databaseFactory)
        {
            this._sysUserRepo = sysUserRepo;
            this._sysUserLogOnRepository = sysUserLogOnRepository;
            this._sysRoleRepository = sysRoleRepository;
            this._sysUserRoleRepository = sysUserRoleRepository;
        }

        /// <summary>
        /// Gửi tạo mới, cập nhật người dùng hệ thống
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="roleList"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity, string keyValue)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                SysUserEntity oldEntity = null;   //Entity trước khi thay đổi

                if (!string.IsNullOrEmpty(keyValue))
                {
                    if (_sysUserRepo.IQueryable().Any(p => p.TopOrganizeId == userEntity.TopOrganizeId && p.Id != keyValue
                        && p.Account == userEntity.Account))
                    {
                        throw new FailedException("登录账号不可重复");
                    }

                    oldEntity = _sysUserRepo.FindEntity(t => t.Id == keyValue);
                    _sysUserRepo.DetacheEntity(oldEntity);

                    userEntity.Modify(keyValue);

                    db.Update(userEntity);
                }
                else
                {
                    if (_sysUserRepo.IQueryable().Any(p => p.TopOrganizeId == userEntity.TopOrganizeId
       && p.Account == userEntity.Account))
                    {
                        throw new FailedException("登录账号不可重复");
                    }

                    userEntity.Create(true);

                    userLogOnEntity.UserId = userEntity.Id;
                    userLogOnEntity.UserSecretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
                    userLogOnEntity.UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.UserPassword, 32).ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();
                    userLogOnEntity.Create(true);
                    db.Insert(userEntity);
                    db.Insert(userLogOnEntity);
                }

                db.Commit();

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, userEntity, SysUserEntity.GetTableName(), oldEntity.Id);
                }
            }
        }

        /// <summary>
        /// Lấy người dùng hệ thống (đăng nhập)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysUserVO> GetPagintionList(Pagination pagination, string OrganizeId, string keyword = null)
        {
            var sb = new StringBuilder();
            var pars = new List<SqlParameter>();
            sb.Append(@"select a.Id,a.Account,c.gh,c.Name,a.CreateTime,a.zt,c.[Description]
,org.Id OrganizeId,org.Name OrganizeName, dept.Name DepartmentName
,logon.Locked
,a.CreatorCode
,a.LastModifyTime
,a.LastModifierCode
,a.px
from Sys_User a
left join Sys_UserStaff b
on a.Id = b.UserId
left join Sys_Staff c
on b.StaffId = c.Id
left join Sys_Organize org
on c.OrganizeId = org.Id
left join Sys_Department dept
on c.DepartmentCode = dept.Code and c.OrganizeId = dept.OrganizeId
left join Sys_UserLogOn logon
on a.Id = logon.UserId
where 1 = 1
 ");
            if (OrganizeId == Constants.TopOrganizeId)  //Nếu là tổ chức cấp cao nhất
            {
                //Hiển thị tất cả người dùng
                sb.Append(" and a.TopOrganizeId = @topOrganizeId and a.Account != 'admin' ");
                pars.Add(new SqlParameter("@topOrganizeId", OrganizeId));
            }
            else
            {
                //Chỉ hiển thị người dùng của cơ sở đó
                sb.Append(" and c.OrganizeId = @OrganizeId and c.Id is not null ");
                pars.Add(new SqlParameter("@OrganizeId", OrganizeId));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars.Add(new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%"));
                sb.Append(" and (a.Account like @searchkeyword or c.Name like @searchkeyword or c.gh like @searchkeyword or c.MobilePhone like @searchkeyword)");
            }

            return this.QueryWithPage<SysUserVO>(sb.ToString(), pagination, pars.ToArray());
        }

        /// <summary>
        /// Lấy nhân viên hệ thống theo Id người dùng hệ thống (đăng nhập)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysStaffEntity GetStaffByUserId(string userId)
        {
            var sql = @"select * from Sys_Staff a
left join Sys_User b
on a.Id = b.StaffId
where b.Id = @userId";

            return this.FirstOrDefault<SysStaffEntity>(sql, new SqlParameter[] {
                new SqlParameter("@userId", userId ?? "")
            });
        }

        /// <summary>
        /// Lấy thông tin nhân viên hệ thống
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysUserVO GetSysUserByUserId(string userId)
        {
            //            var sql = @"select a.*, b.Name, b.gh, b.DepartmentCode from Sys_User a
            //left join Sys_Staff b
            //on a.StaffId = b.Id
            //where a.Id = @userId";
            var sql = @"select a.* from Sys_User a
where a.Id = @userId";

            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@userId", userId)
            };

            return this.FirstOrDefault<SysUserVO>(sql, par);
        }

        /// <summary>
        /// Lấy danh sách người dùng thuộc vai trò
        /// </summary>
        /// <param name="roleId"></param>
        public IList<RoleUserVO> GetPagintionUserListByRoleId(Pagination pagination, string gh, string name, string roleId, string topOrganizeId)
        {
            var sql = @"select distinct c.Id UserId, d.gh, d.Name UserName,d.Gender, d.DepartmentCode, d.OrganizeId
from Sys_UserRole a
left join Sys_Role b
on a.RoleId = b.Id
left join Sys_User c
on a.UserId = c.Id
left join Sys_Staff d
on c.StaffId = d.Id
where b.Id is not null and c.Id is not null and d.Id is not null
and a.RoleId = @roleId
and (isnull(@gh,'') = '' or d.gh like @searchgh)
and (isnull(@name,'') = '' or d.Name like @searchname)
and c.TopOrganizeId = @topOrganizeId and d.TopOrganizeId = @topOrganizeId";

            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@roleId", roleId ?? "")
                ,new SqlParameter("@topOrganizeId", topOrganizeId)
                ,new SqlParameter("@gh", gh ?? "")
                ,new SqlParameter("@searchgh", "%"+(gh??"")+"%")
                ,new SqlParameter("@name", name ?? "")
                ,new SqlParameter("@searchname", "%"+(name??"")+"%")
            };

            return this.QueryWithPage<RoleUserVO>(sql, pagination, par);
        }

        /// <summary>
        /// Tạo người dùng mới
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        public void CreateUser(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Insert(userEntity);
                db.Insert(userLogOnEntity);
                db.Commit();
            }
        }

        /// <summary>
        /// Lấy danh sách nhân viên hệ thống liên kết theo UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysStaffEntity> GetStaffListByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            var sql = @"select distinct b.* from Sys_UserStaff a WITH(NOLOCK)
left join Sys_Staff b WITH(NOLOCK)
on a.StaffId = b.Id
where a.UserId = @userId";
            return this.FindList<SysStaffEntity>(sql, new[] { new SqlParameter("@userId", userId) });
        }

        /// <summary>
        /// Lấy VO nhân viên hệ thống theo OrganizeId (và có SysUser tương ứng)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<SysUserStaffVO> GetSatffVOListByOrg(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            var sql = @"select c.Id UserId, c.Account UserCode, a.Id StaffId, a.gh gh, a.DepartmentCode, a.Name  
from Sys_Staff a
left join Sys_UserStaff b
on b.StaffId = a.Id
left join Sys_User c
on b.UserId = c.Id
where 
a.zt = '1' and b.zt = '1' and c.zt = '1'
and c.TopOrganizeId = @topOrgId
and c.Id is not null
and a.OrganizeId = @orgId";
            return this.FindList<SysUserStaffVO>(sql, new[] { new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@topOrgId", Constants.TopOrganizeId) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetLanguageTypeByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            var sql = "select LanguageType from Sys_User(nolock) where Id = @userId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@userId", userId) });
        }

        /// <summary>
        /// Lấy tất cả các vị trí theo Id nhân viên
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="StaffId"></param>
        /// <returns></returns>
        public IList<string> GetDutyListByStaffId(string orgId, string StaffId)
        {
            var sql = @"select c.Code from Sys_StaffDuty a
left join Sys_Staff b
on a.StaffId = b.Id
left join Sys_Duty c
on a.DutyId = c.Id
where b.Id = @StaffId and b.OrganizeId = @orgId
and a.zt = '1' and b.zt = '1' and c.zt = '1'";
            return this.FindList<string>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@StaffId", StaffId) });
        }

        /// <summary>
        /// Đặt lại mật khẩu
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        public void RevisePassword(string userPassword, string userId)
        {
            var secretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
            var password = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), secretkey).ToLower(), 32).ToLower();

            //Ở đây không trực tiếp thao tác với bảng thì không làm được, không thể thông qua view
            this.ExecuteSqlCommand("update sys_userlogon set UserSecretkey = @secretkey, UserPassword = @password where UserId = @userId and zt = '1'", new[] {
                new SqlParameter("@userId", userId)
                ,new SqlParameter("@secretkey", secretkey)
                ,new SqlParameter("@password", password)
            });
        }

        public string GetYbdmByGh(string rygh)
        {
            string sql = @"  select gjybdm from NewtouchHIS_Base..Sys_Staff where gh=@rygh";

            return FirstOrDefault<string>(sql, new[] { new SqlParameter("@rygh", rygh) });
        }
    }
}
