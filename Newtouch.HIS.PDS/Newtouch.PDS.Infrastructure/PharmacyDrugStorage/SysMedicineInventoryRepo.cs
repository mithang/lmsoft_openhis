using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Kiểm kê tồn kho xt_yp_pdxx
    /// </summary>
    public class SysMedicineInventoryRepo : RepositoryBase<SysMedicineInventoryEntity>, ISysMedicineInventoryRepo
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="databaseFactory"></param>
        
        /// <summary>
        /// Kiểm tra có kiểm kê chưa hoàn thành không （Bắt đầu）
        /// </summary>
        /// <returns></returns>
        public int SelectUnfinishedInventory()
        {
            var organizeId = OperatorProvider.GetCurrent().OrganizeId;
            return IQueryable().Count(a => a.yfbmCode == Constants.CurrentYfbm.yfbmCode && a.Jssj == null && a.OrganizeId == organizeId);
        }

        /// <summary>
        /// Kiểm tra có kiểm kê chưa hoàn thành không （Kết thúc）
        /// </summary>
        /// <returns></returns>
        public int SelectUnfinishedInventoryByPdId(string pdId)
        {
            var organizeId = OperatorProvider.GetCurrent().OrganizeId;
            return IQueryable().Count(a => a.yfbmCode == Constants.CurrentYfbm.yfbmCode && a.Jssj == null && a.pdId == pdId && a.OrganizeId == organizeId);
        }

        /// <summary>
        /// Theo kiểm kêIDLấy thông tin kiểm kê
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public SysMedicineInventoryEntity SelectPdInfByPdId(string pdId, string organizeId)
        {
            return IQueryable().FirstOrDefault(p => p.yfbmCode == Constants.CurrentYfbm.yfbmCode && p.pdId == pdId && p.OrganizeId == organizeId);
        }
    }
}
