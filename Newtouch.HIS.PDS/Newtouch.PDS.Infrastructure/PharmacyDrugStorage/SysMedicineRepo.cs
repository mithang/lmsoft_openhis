using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using SqlSugar;
using System.Data.Common;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Thuốc hệ thống V_S_xt_yp
    /// </summary>
    public class SysMedicineRepo : RepositoryBase<SysMedicineVEntity>, ISysMedicineRepo
    {
        /// <summary>
        /// Khởi tạo hàm tạo
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysMedicineRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy một đối tượng thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public SysMedicineVEntity GetMedicineByCode(string orgId, string ypCode)
        {
            const string sql = "select * from [NewtouchHIS_Base]..V_S_xt_yp(nolock) where OrganizeId = @orgId and ypCode = @ypCode";
            return FirstOrDefault<SysMedicineVEntity>(sql, new SugarParameter[] { new SugarParameter("@orgId", orgId), new SugarParameter("@ypCode", ypCode) });
        }

        /// <summary>
        /// Lấy một tập hợp thuốc strwhere Tạm thời không hiệu lực
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<SysMedicineVEntity> GetMedicineListByOrg(string orgId)
        {
            const string sql = "select * from [NewtouchHIS_Base]..V_S_xt_yp(nolock) where OrganizeId = @orgId";
            return FindList<SysMedicineVEntity>(sql, new SugarParameter[] { new SugarParameter("@orgId", orgId) });
        }
    }
}
