using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Chi tiết phiếu đề nghị trả thuốc nội trú
    /// </summary>
    public class ZyReturnDrugApplyBillDetailRepo : RepositoryBase<ZyReturnDrugApplyBillDetailEntity>, IZyReturnDrugApplyBillDetailRepo
    {
        public ZyReturnDrugApplyBillDetailRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Theo đề nghị trả thuốcIdLấy chi tiết đề nghị trả thuốc
        /// </summary>
        /// <param name="rabId"></param>
        /// <returns></returns>
        public List<ZyReturnDrugApplyBillDetailEntity> SelectData(string rabId)
        {
            const string sql = @"SELECT * FROM dbo.zy_returnDrugApplyBillDetail(NOLOCK) WHERE rabId=@rabId AND zt='1' AND tysl>0";
            var param = new SqlParameter("@rabId", rabId);
            return _dataContext.Database.SqlQuery<ZyReturnDrugApplyBillDetailEntity>(sql, param).ToList();
        }
    }
}