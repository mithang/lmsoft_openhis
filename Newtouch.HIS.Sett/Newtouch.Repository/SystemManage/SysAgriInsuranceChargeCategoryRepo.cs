using Newtouch.HIS.Domain.IRepository;
using System.Data.SqlClient;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// bảo hiểm nông nghiệp thu phí loại lớn 
    /// </summary>
    public class SysAgriInsuranceChargeCategoryRepo : RepositoryBase<SysAgriInsuranceChargeCategoryVEntity>, ISysAgriInsuranceChargeCategoryRepo
    {
        public SysAgriInsuranceChargeCategoryRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// Lấy tất cả các bảo hiểm nông nghiệp thu phí hộp thả xuống loại lớn 
        /// </summary>
        /// <returns></returns>
        public IList<SysAgriInsuranceChargeCategoryVEntity> GetnbdlSelect(string orgId)
        {
            var sql = "select dlCode,dlmc,py from [NewtouchHIS_Base]..V_S_xt_nbsfdl width(nolock) where OrganizeId = @orgId and zt = '1'";
            return this.FindList<SysAgriInsuranceChargeCategoryVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

    }
}
