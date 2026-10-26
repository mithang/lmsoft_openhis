using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;
using System;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_kcjz
    /// </summary>
    public class SysMedicineStockCarryOverRepo : RepositoryBase<SysMedicineStockCarryOverEntity>, ISysMedicineStockCarryOverRepo
    {
        public SysMedicineStockCarryOverRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Tra cứu thời gian kết chuyển lần trước
        /// </summary>
        /// <returns></returns>
        public CarryOverLastAccountPeriodAndCarrayTimeVO GetLastAccountPeriodAndCarrayTime()
        {
            var lastEntity = new CarryOverLastAccountPeriodAndCarrayTimeVO();  //Thời gian kết chuyển lần trước
            var organizeId = Common.Operator.OperatorProvider.GetCurrent().OrganizeId;
            var yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var entity = IQueryable().Where(a => a.zt == "1" && a.OrganizeId == organizeId && a.yfbmCode == yfbmCode);
            if (!entity.Any())    //Chưa từng kết chuyển
            {
                lastEntity.lastCarrayTime = DateTime.Now.ToString("yyyy-01-01 00:00:00");
            }
            else
            {
                var entity2 = entity.OrderByDescending(a => a.jssj);
                lastEntity.lastCarrayTime = entity2.Select(a => a.jssj).FirstOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
                lastEntity.lastAccountPeriod = entity2.Select(a => a.zq).FirstOrDefault();
                if (lastEntity.lastCarrayTime == null)
                {
                    throw new FailedException("获取上一次结转时间失败");
                }
            }
            return lastEntity;
        }


    }
}
