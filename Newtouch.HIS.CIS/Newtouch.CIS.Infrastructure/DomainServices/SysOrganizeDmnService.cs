using System.Data.SqlClient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Repository;

namespace Newtouch.DomainServices
{
    public class SysOrganizeDmnService : DmnServiceBase, ISysOrganizeDmnService
    {
        public string GetCodeByOrgId(string orgId)
        {
            var sql = "SELECT Code FROM Sys_Organize WHERE Id = @id";
            return BaseDb.GetConnection().Ado.GetString(sql,
                Newtouch.CIS.Compat.SqlParam.ToSugar(new SqlParameter[] {
                    new SqlParameter("@id", orgId ?? "") }));
        }
    }
}
