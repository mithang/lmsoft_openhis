using System.Collections.Generic;
using Newtouch.CIS.Compat;
using System.Data.Common;
using System.Data.SqlClient;

using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Đơn thuốcchi tiết
    /// </summary>
    public class PrescriptionDetailRepo : RepositoryBase<PrescriptionDetailEntity>, IPrescriptionDetailRepo
    {
        public PrescriptionDetailRepo()
            : base()
        {
        }

        /// <summary>
        /// LấyĐơn thuốcchi tiết
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
        /// LấyĐơn thuốcchi tiết
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
