using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// bộ phận thuốc
    /// </summary>
    public interface IDepartmentMedicineInfoDmnService
    {
        /// <summary>
        /// lấy nhà thuốc của thuốc số lượng tồn kho và đơn vị
        /// </summary>
        /// <param name="yfbmCode">hiện tại bộ phận nhà thuốc</param>
        /// <param name="ypCode">mã thuốc</param>
        /// <param name="orgId">tổ chức</param>
        /// <param name="fybm">bộ phận phát thuốc</param>
        /// <returns></returns>
        DepartmentMedicineStockUnitVO GetYpKcslAndYpdw(string yfbmCode, string ypCode, string orgId, string fybm);

        /// <summary>
        /// lấy lĩnh nội bộ thuốc nguồn dữ liệu
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fybm"></param>
        /// <param name="orgId"></param>
        IList<RequisitionDepartmentMedicineSeleVO> GetRequisitionDepartmentMedicineSeleVOList(string keyword, string fybm, string orgId);
    }
}
