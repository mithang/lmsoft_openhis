using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Chi tiết đơn thuốc
    /// </summary>
    public class PrescriptionDetailRepo : RepositoryBase<PrescriptionDetailEntity>, IPrescriptionDetailRepo
    {
        public PrescriptionDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy chi tiết đơn thuốc
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        public List<PrescriptionDetailEntity> SelectData(string cfId, bool djbz)
        {
            if (djbz)
            {
                const string sql = @"SELECT * FROM dbo.xt_cfmx(NOLOCK) WHERE cfId=@cfId AND zt='1' and xmcode is null";
                var param = new DbParameter[] { new SqlParameter("@cfId", cfId) };
                return FindList<PrescriptionDetailEntity>(sql, param);
            }
            else
            {
                const string sql = @"SELECT * FROM dbo.xt_cfmx(NOLOCK) WHERE cfId=@cfId AND zt='1' ";
                var param = new DbParameter[] { new SqlParameter("@cfId", cfId) };
                return FindList<PrescriptionDetailEntity>(sql, param);
            }

        }
        /// <summary>
        /// Lấy chi tiết đơn thuốc
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        public List<PrescriptionDetailEntity> SelectDataDjxm(string cfId)
        {
            const string sql = @"SELECT * FROM dbo.xt_cfmx(NOLOCK) WHERE cfId=@cfId AND zt='1' and xmcode is not null";
            var param = new DbParameter[] { new SqlParameter("@cfId", cfId) };
            return FindList<PrescriptionDetailEntity>(sql, param);
        }
    }
}
