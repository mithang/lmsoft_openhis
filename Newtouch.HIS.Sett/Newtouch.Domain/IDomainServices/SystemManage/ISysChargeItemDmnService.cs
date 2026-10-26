using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysChargeItemDmnService
    {
        /// <summary>
        /// Tìm kiếm hạng mục
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <param name="xmzlfzlbz">Cờ điều trị/không điều trị của hạng mục:true Hạng mục điều trị false Hạng mục không điều trị nullĐều bao gồm</param>
        /// <param name="mzzybz"></param>
        /// <returns></returns>
        IList<SysChargeItemVO> SelectSearch(string keyword, string orgId, bool? xmzlfzlbz = true, string mzzybz = null, string dlCode = null, bool isContansChildDl = true);

        /// <summary>
        /// Hạng mụcAndTìm kiếm thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <param name="zlfzlbz">Cờ điều trị/không điều trị của hạng mục:true Hạng mục điều trị false Hạng mục không điều trị nullĐều bao gồm</param>
        /// <param name="mzzybz"></param>
        /// <returns></returns>
        IList<SysChargeItemAndMedicineVO> SelectItemAndMedicineSearch(string keyword, string orgId, bool? zlfzlbz = true, string mzzybz = null);

        /// <summary>
        /// Lấy loại lớn
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysChargeClassVO> GetSFDLList(string orgId);

        /// <summary>
        /// Trang mẫu thu phíindex list
        /// </summary>
        /// <returns></returns>
        IList<SysChargeTemplateGridVO> Search(Pagination pagination, string keyword, string organizeId, string yxbz);

        /// <summary>
        /// Lấy cách dùng
        /// </summary>
        /// <returns></returns>
        IList<SysUsageVO> GetYF();
    }

}
