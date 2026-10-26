using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospAccountingPlanDetailRepo : RepositoryBase<HospAccountingPlanDetailEntity>, IHospAccountingPlanDetailRepo
    {
        public HospAccountingPlanDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Quyết toán xuất viện giờ kiểm tra có hay không có chưa hoàn thành và chưa chấm dứt của kế hoạch thực hiện 
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        public void CheckAccountingPlanDetailStatus(string zyh,string orgId)
        {
            if(string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var list = this.IQueryable().Where(a => a.zyh == zyh && a.OrganizeId == orgId && a.zt == "1" && (a.zxzt != (int)EnumJzjhZXZT.Stopped && a.zxzt != (int)EnumJzjhZXZT.Finished)).ToList();
            if (list != null && list.Count > 0)
            {
                throw new FailedCodeException("HOPS_THERE_IS_AN_UNSETTLED_ACCOUNTING_PLAN");
            }
        }
    }
}
