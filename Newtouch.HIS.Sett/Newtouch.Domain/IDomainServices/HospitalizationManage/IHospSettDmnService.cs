using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Quyết toán nội trú Giao diện
    /// </summary>
    public interface IHospSettDmnService
    {
        /// <summary>
        /// Quyết toán nội trú Lưu vào CSDL
        /// </summary>
        /// <param name="vo"></param>
        void SettSyncToDB(OutHospSettDBDataUpdateCollectVO vo, string orgId);

        /// <summary>
        /// Quyết toán nội trú Hủy quyết toán Lưu vào CSDL
        /// </summary>
        /// <param name="vo"></param>
        void CancelSettSyncToDB(HospCancelSettDBDataUpdateCollectVO vo, string orgId);

        #region 住院结算查询
        IList<HospSettQueryGridVO> GridInPatientQueryGridJson(Pagination pagination, HospSettQueryReq req, string orgId);
        #endregion

        #region （常规医院）HIS住院结算查询
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <param name="fph"></param>
        /// <param name="jsksrq"></param>
        /// <param name="jsjsrq"></param>
        /// <returns></returns>
        IList<HospSettlementInfoVO> GetPaginationSettlementList(Pagination pagination, string organizeId, string keyword, string fph, DateTime? jsksrq, DateTime? jsjsrq);

        /// <summary>
        /// Thông tin bệnh nhân quyết toán tự chi trả chờ tải lên
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="jsksrq"></param>
        /// <param name="jsjsrq"></param>
        /// <returns></returns>
        IList<HospSettlementInfoVO> GetPaginationZFSettlementList(Pagination pagination, string organizeId, string keyword, DateTime? jsksrq, DateTime? jsjsrq);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        IList<HospSettlementClassificationFeeVO> SettlementDetailsQuery(string organizeId, int jsnm);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        void UpdateSettInvoiceNo(string orgId, int jsnm, string fph);
        /// <summary>
        /// Tra cứu chi tiết chi phí quyết toán xuất viện
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfdl"></param>
        /// <param name="mc"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> SettlementDetailsItemsQuery(Pagination pagination, string zyh, string orgId, string sfdl, string jsnms, string mc);
        #endregion 
    }
}
