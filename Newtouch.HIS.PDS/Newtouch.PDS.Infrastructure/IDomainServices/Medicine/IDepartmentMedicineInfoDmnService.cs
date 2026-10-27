using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Bộ phận Thuốc
    /// </summary>
    public interface IDepartmentMedicineInfoDmnService
    {
        /// <summary>
        /// Lấy thuốc của nhà thuốc Số lượng và đơn vị tồn kho
        /// </summary>
        /// <param name="yfbmCode">Bộ phận nhà thuốc hiện tại</param>
        /// <param name="ypCode">Mã thuốc</param>
        /// <param name="orgId">Tổ chức</param>
        /// <param name="fybm">Bộ phận phát thuốc</param>
        /// <returns></returns>
        DepartmentMedicineStockUnitVO GetYpKcslAndYpdw(string yfbmCode, string ypCode, string orgId, string fybm);

        /// <summary>
        /// Lấy Lĩnh nội bộ Nguồn dữ liệu thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fybm"></param>
        /// <param name="orgId"></param>
        IList<RequisitionDepartmentMedicineSeleVO> GetRequisitionDepartmentMedicineSeleVOList(string keyword, string fybm, string orgId);
    }
}
