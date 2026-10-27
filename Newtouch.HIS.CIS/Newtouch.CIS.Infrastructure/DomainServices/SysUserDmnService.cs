using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Newtouch.CIS.Repository;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Repository;

namespace Newtouch.DomainServices
{
    public class SysUserDmnService : DmnServiceBase, ISysUserDmnService
    {
        public bool CheckStaffIsBelongDuty(string staffId, string duty)
        {
            if (string.IsNullOrWhiteSpace(staffId) || string.IsNullOrWhiteSpace(duty)) return false;
            var sql = "SELECT COUNT(*) FROM V_C_Sys_StaffDuty WHERE zt = '1' AND DutyCode = @duty AND (StaffId = @sid OR StaffGh = @sid)";
            var n = BaseDb.GetConnection().Ado.GetInt(sql,
                Newtouch.CIS.Compat.SqlParam.ToSugar(new SqlParameter[] {
                    new SqlParameter("@duty", duty), new SqlParameter("@sid", staffId) }));
            return n > 0;
        }

        public IList<SysStaffEntity> GetSatffList(string orgId, string keyword)
        {
            var sql = "SELECT * FROM Sys_Staff WHERE zt = '1' AND OrganizeId = @orgId AND (@kw = '' OR Name LIKE @like OR py LIKE @like OR gh LIKE @like)";
            return BaseDb.GetConnection().Ado.SqlQuery<SysStaffEntity>(sql,
                Newtouch.CIS.Compat.SqlParam.ToSugar(new SqlParameter[] {
                    new SqlParameter("@orgId", orgId ?? ""),
                    new SqlParameter("@kw", keyword ?? ""),
                    new SqlParameter("@like", "%" + (keyword ?? "") + "%") })).ToList();
        }

        public IList<SysStaffEntity> GetStaffListByOrg(string orgId)
        {
            var sql = "SELECT * FROM Sys_Staff WHERE zt = '1' AND OrganizeId = @orgId";
            return BaseDb.GetConnection().Ado.SqlQuery<SysStaffEntity>(sql,
                Newtouch.CIS.Compat.SqlParam.ToSugar(new SqlParameter[] {
                    new SqlParameter("@orgId", orgId ?? "") })).ToList();
        }

        public IList<StaffDutyVO> GetStaffByDutyCode(string orgId, string dutyCode)
        {
            var sql = "SELECT * FROM V_C_Sys_StaffDuty WHERE zt = '1' AND OrganizeId = @orgId AND DutyCode = @duty";
            return BaseDb.GetConnection().Ado.SqlQuery<StaffDutyVO>(sql,
                Newtouch.CIS.Compat.SqlParam.ToSugar(new SqlParameter[] {
                    new SqlParameter("@orgId", orgId ?? ""),
                    new SqlParameter("@duty", dutyCode ?? "") })).ToList();
        }

        public IList<StaffDutyVO> GetStaffDutyListByOrganizeId(string orgId)
        {
            var sql = "SELECT * FROM V_C_Sys_StaffDuty WHERE zt = '1' AND OrganizeId = @orgId";
            return BaseDb.GetConnection().Ado.SqlQuery<StaffDutyVO>(sql,
                Newtouch.CIS.Compat.SqlParam.ToSugar(new SqlParameter[] {
                    new SqlParameter("@orgId", orgId ?? "") })).ToList();
        }
    }
}
