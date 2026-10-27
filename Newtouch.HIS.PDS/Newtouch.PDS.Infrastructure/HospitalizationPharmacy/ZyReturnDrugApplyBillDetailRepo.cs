using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Chi tiết đơn xin trả thuốc nội trú
    /// </summary>
    public class ZyReturnDrugApplyBillDetailRepo : RepositoryBase<ZyReturnDrugApplyBillDetailEntity>, IZyReturnDrugApplyBillDetailRepo
    {
        public ZyReturnDrugApplyBillDetailRepo() { }
        public ZyReturnDrugApplyBillDetailRepo(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) : base(databaseFactory) { }
        
        /// <summary>
        /// Theo đơn xin trả thuốcIdLấy chi tiết đơn xin trả thuốc
        /// </summary>
        /// <param name="rabId"></param>
        /// <returns></returns>
        public List<ZyReturnDrugApplyBillDetailEntity> SelectData(string rabId)
        {
            const string sql = @"SELECT * FROM dbo.zy_returnDrugApplyBillDetail(NOLOCK) WHERE rabId=@rabId AND zt='1' AND tysl>0";
            var param = new SugarParameter("@rabId", rabId);
            return _dataContext.Database.SqlQuery<ZyReturnDrugApplyBillDetailEntity>(sql, param).ToList();
        }
    }
}