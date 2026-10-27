using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Lĩnh nội bộ
    /// </summary>
    public interface IRequisitionDmnService
    {
        /// <summary>
        /// Lĩnh mới Gửi và lưu
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="fybmCode"></param>
        /// <param name="slbmCode"></param>
        /// <param name="mxList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool SubmitRequisition(string sldh, string fybmCode, string slbmCode, IList<RequisitionDepartmentMedicineSubmitItemVO> mxList, string orgId);

        /// <summary>
        /// Đơn lĩnh nội bộ Truy vấn
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sldh"></param>
        /// <param name="slbm">Bộ phận lĩnh</param>
        /// <param name="ckbm">Bộ phận xuất kho</param>
        /// <param name="ffzt">Trạng thái phát</param>
        /// <param name="allUseableFfzt"></param>
        /// <returns></returns>
        IList<RequisitionSelectVO> RequisitionSearch(Pagination pagination, string orgId, DateTime? startDate, DateTime? endDate, string sldh, string slbm, string ckbm, int? ffzt, string allUseableFfzt);

        /// <summary>
        /// Đơn lĩnh nội bộ Truy vấn chi tiết
        /// </summary>
        /// <param name="sldId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<RequisitionSelectDetailVO> RequisitionDetailListBySlId(string sldId, string orgId);

    }
}
