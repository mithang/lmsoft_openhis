
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Đ ơn thuốc bảng chính 
    /// </summary>
    public class OutpatientPrescriptionRepo : RepositoryBase<OutpatientPrescriptionEntity>, IOutpatientPrescriptionRepo
    {
        public OutpatientPrescriptionRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// theo cfh lấy hiệu lực Đ ơn thuốc thực thể
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfh"></param>
        /// <returns></returns>
        public OutpatientPrescriptionEntity GetValidEntityByCfh(string orgId, string cfh)
        {
            var sql = @"select * from mz_cf where cfh = @cfh and OrganizeId = @orgId";
            return this.FirstOrDefault<OutpatientPrescriptionEntity>(sql
                , new[] { new SqlParameter("@cfh", cfh ?? ""), new SqlParameter("@orgId", orgId ?? "") });
        }

        /// <summary>
        /// theo cfnm lấy hiệu lực Đ ơn thuốc thực thể
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>
        public OutpatientPrescriptionEntity GetValidEntityByCfnm(string orgId, int cfnm)
        {
            var sql = @"select * from mz_cf where cfnm = @cfnm and OrganizeId = @orgId";
            return this.FirstOrDefault<OutpatientPrescriptionEntity>(sql
                , new[] { new SqlParameter("@cfnm", cfnm), new SqlParameter("@orgId", orgId ?? "") });
        }

        /// <summary>
        /// Tạo mới số đ ơn thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="type">Loại đ ơn thuốc1 thuốc 2 không phải thuốc</param>
        /// <returns></returns>
        public string GeneratePresNo(string orgId, int type)
        {
            var cfh = "R" + DateTime.Now.ToString("yyyyMMdd") + type.ToString()
                + "N" + EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("mz_cf.cfh", orgId, "{0:D5}", true);
            return cfh;
        }

    }
}
