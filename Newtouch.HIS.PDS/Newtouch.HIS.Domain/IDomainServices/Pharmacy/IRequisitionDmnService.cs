using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// lĩnh nội bộ
    /// </summary>
    public interface IRequisitionDmnService
    {
        /// <summary>
        /// Lĩnh mới gửi lưu
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="fybmCode"></param>
        /// <param name="slbmCode"></param>
        /// <param name="mxList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool SubmitRequisition(string sldh, string fybmCode, string slbmCode, IList<RequisitionDepartmentMedicineSubmitItemVO> mxList, string orgId);

        /// <summary>
        /// lĩnh nội bộ đơn tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sldh"></param>
        /// <param name="slbm">bộ phận lĩnh</param>
        /// <param name="ckbm">bộ phận xuất kho</param>
        /// <param name="ffzt">trạng thái cấp phát</param>
        /// <param name="allUseableFfzt"></param>
        /// <returns></returns>
        IList<RequisitionSelectVO> RequisitionSearch(Pagination pagination, string orgId, DateTime? startDate, DateTime? endDate, string sldh, string slbm, string ckbm, int? ffzt, string allUseableFfzt);

        /// <summary>
        /// lĩnh nội bộ đơn chi tiết tra cứu
        /// </summary>
        /// <param name="sldId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<RequisitionSelectDetailVO> RequisitionDetailListBySlId(string sldId, string orgId);

    }
}
