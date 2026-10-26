using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure.TSQL;
using System.Data.Common;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices.OutOrInStoredOperate
{
    /// <summary>
    /// Thao tác tồn kho
    /// </summary>
    public class KcxxDmnService : DmnServiceBase, IKcxxDmnService
    {
        public KcxxDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Đóng băng tồn kho, trừ tồn kho, dùng cho trả lại phát thuốc nội bộ
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
            var param = new DbParameter[] {
                new SqlParameter("@ypdm", ypdm),
                new SqlParameter("@Organizeid", organizeid),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph", ph),
                new SqlParameter("@sl", sl),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@rkbm", rkbm),
            };
            return FirstOrDefault<string>(TSqlStock.frozen_stock_Reduce_use_by_returninward, param);
        }

        /// <summary>
        /// Đóng băng tồn kho, trừ tồn kho, dùng cho xuất kho trực tiếp
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
            var param = new DbParameter[] {
                new SqlParameter("@ypdm", ypdm),
                new SqlParameter("@Organizeid", organizeid),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph", ph),
                new SqlParameter("@sl", sl),
                new SqlParameter("@yfbmCode", yfbmCode)
            };
            return FirstOrDefault<string>(TSqlStock.frozen_stock_Reduce_use_by_deliveryDirect, param);
        }

        /// <summary>
        /// Trừ tồn kho
        /// </summary>
        /// <param name="ypdm">Mã thuốc</param>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất</param>
        /// <param name="pc">Lô</param>
        /// <param name="ph">Số lô</param>
        /// <param name="yfbmCode">Bộ phận nhà thuốc cần trừ tồn kho</param>
        /// <param name="organizeid">Tổ chức</param>
        /// <returns></returns>
        public string SubtractStock(string ypdm, int sl, string pc, string ph, string yfbmCode, string organizeid)
        {
            var param = new DbParameter[] {
                new SqlParameter("@ypdm", ypdm),
                new SqlParameter("@Organizeid", organizeid),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph", ph),
                new SqlParameter("@sl", sl),
                new SqlParameter("@yfbmCode", yfbmCode)
            };
            return FirstOrDefault<string>(TSqlStock.SubtractStock, param);
        }
    }
}
