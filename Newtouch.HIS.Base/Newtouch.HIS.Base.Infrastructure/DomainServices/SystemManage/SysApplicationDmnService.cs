using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class SysApplicationDmnService : DmnServiceBase, ISysApplicationDmnService
    {
        public SysApplicationDmnService()
            
        {
        }

        /// <summary>
        /// Lấy danh sách tổ chức đã được ủy quyền cho ứng dụng
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public IList<SysOrganizeEntity> GetAuthedOrgListByAppId(string appId)
        {
            var sql = @"if exists (select 1 from Sys_OrganizeApplication where ApplicationId = @appId and OrganizeId = '*' and zt = '1')
begin
    --Tất cả tổ chức cấp cao nhất
	select * from Sys_Organize where zt = '1'
end
else
	select distinct b.* from Sys_OrganizeApplication a
	left join Sys_Organize b
	on a.OrganizeId = b.Id
	where (a.ApplicationId = @appId or a.ApplicationId = '*') and a.zt = '1' and b.zt = '1'";

            return this.FindList<SysOrganizeEntity>(sql, new SqlParameter[] {
                new SqlParameter("@appId", appId),
            });
        }

        /// <summary>
        /// Ủy quyền ứng dụng cho tổ chức được chỉ định
        /// </summary>
        /// <param name="appId"></param>
        public void UpdateAuthOrganizeList(string appId, string orgList, string curUserCode)
        {
            var orgIdArr = orgList.Trim(',');
            if (string.IsNullOrWhiteSpace(orgIdArr))
            {
                throw new FailedException("Tham số không hợp lệ.orgList");
            }
            var sql = @"
delete from Sys_OrganizeApplication where ApplicationId = @appId
insert into Sys_OrganizeApplication(Id, OrganizeId, ApplicationId, CreateTime, CreatorCode,zt)
select newid(), Id, @appId, GETDATE(), @creatorCode, '1'
from Sys_Organize where Id in (
    select value from dbo.SplitToTable(@orgIdArr, ',')
)";
            Sql.ExecuteCommand(sql, new SqlParameter[] {
                new SqlParameter("@appId", appId),
                new SqlParameter("@creatorCode", curUserCode),
                new SqlParameter("@orgIdArr", SqlDbType.VarChar, -1) { Value = orgIdArr }
            });
        }

        /// <summary>
        /// Ủy quyền ứng dụng cho tất cả tổ chức
        /// </summary>
        /// <param name="appId"></param>
        public void AuthAllOrganize(string appId, string curUserCode)
        {
            var sql = @"
if exists (select 1 from Sys_Application where Id = @appId)
begin
    delete from Sys_OrganizeApplication where ApplicationId = @appId
    insert into Sys_OrganizeApplication(Id, OrganizeId, ApplicationId, CreateTime, CreatorCode,zt)
    values(newid(), '*', @appId, GETDATE(), @creatorCode, '1')
end";
            Sql.ExecuteCommand(sql, new SqlParameter[] {
                new SqlParameter("@appId", appId),
                new SqlParameter("@creatorCode", curUserCode)
            });
        }

        /// <summary>
        /// Hủy tất cả ủy quyền(Tổ chức)
        /// </summary>
        /// <param name="appId"></param>
        public void AuthCancelAllOrganize(string appId)
        {
            var sql = @"delete from Sys_OrganizeApplication where ApplicationId = @appId";
            Sql.ExecuteCommand(sql, new SqlParameter[] {
                new SqlParameter("@appId", appId)
            });
        }

    }
}
