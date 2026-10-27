using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure.TSQL;
using System.Data.Common;
using SqlSugar;

namespace Newtouch.HIS.DomainServices.OutOrInStoredOperate
{
    /// <summary>
    /// 库存操作
    /// </summary>
    public class KcxxDmnService : DmnServiceBase, IKcxxDmnService
    {
        
        /// <summary>
        /// 冻结库存  扣库存  内部发药退回使用
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="organizeid"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="sl"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="rkbm"></param>
        /// <returns></returns>
        public string FrozenStockReduceByReturninward(string ypdm,
            string organizeid,
            string pc,
            string ph,
            int sl,
            string yfbmCode,
            string rkbm)
        {
            var param = new SugarParameter[] {
                new SugarParameter("@ypdm", ypdm),
                new SugarParameter("@Organizeid", organizeid),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph),
                new SugarParameter("@sl", sl),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@rkbm", rkbm),
            };
            return FirstOrDefault<string>(TSqlStock.frozen_stock_Reduce_use_by_returninward, param);
        }

        /// <summary>
        /// 冻结库存  扣库存  直接出库使用
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="organizeid"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="sl"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="rkbm"></param>
        /// <returns></returns>
        public string FrozenStockReduceByDeliveryDirect(string ypdm,
            string organizeid,
            string pc,
            string ph,
            int sl,
            string yfbmCode)
        {
            var param = new SugarParameter[] {
                new SugarParameter("@ypdm", ypdm),
                new SugarParameter("@Organizeid", organizeid),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph),
                new SugarParameter("@sl", sl),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return FirstOrDefault<string>(TSqlStock.frozen_stock_Reduce_use_by_deliveryDirect, param);
        }

        /// <summary>
        /// 扣库存
        /// </summary>
        /// <param name="ypdm">药品代码</param>
        /// <param name="sl">最小单位数量</param>
        /// <param name="pc">批次</param>
        /// <param name="ph">批号</param>
        /// <param name="yfbmCode">需扣库存的药房部门</param>
        /// <param name="organizeid">组织机构</param>
        /// <returns></returns>
        public string SubtractStock(string ypdm, int sl, string pc, string ph, string yfbmCode, string organizeid)
        {
            var param = new SugarParameter[] {
                new SugarParameter("@ypdm", ypdm),
                new SugarParameter("@Organizeid", organizeid),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph),
                new SugarParameter("@sl", sl),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return FirstOrDefault<string>(TSqlStock.SubtractStock, param);
        }
    }
}
