using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Domain.VO;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Trả thuốc
    /// </summary>
    public interface ItyDmnService
    {
        /// <summary>
        /// Truy vấn thông tin thuốc có thể trả
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RefundedDrugVo> SelectRefundedDrugs(string cfh, string yfbmCode, string organizeId);

        /// <summary>
        /// Truy vấn thông tin thuốc có thể trả hết
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RefundedDrugVo> SelectCompleteRefundedDrugs(string cfh, string yfbmCode, string organizeId);

        /// <summary>
        /// Truy vấn số lượng thuốc có thể trả
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns>Số lượng có thể trả đơn vị nhỏ nhất</returns>
        int SelectRefundedSl(string ypCode, string pc, string ph, string yfbmCode, string organizeId);

        /// <summary>
        /// Trả thuốc ngoại trú
        /// </summary>
        /// <param name="tyInfo"></param>
        /// <param name="returnDrugBillNo"></param>
        /// <returns></returns>
        string ReturnDrug(tyInfo tyInfo, out string returnDrugBillNo);

        /// <summary>
        /// Trả thuốc ngoại trú
        /// </summary>
        /// <param name="tyInfo"></param>
        /// <param name="returnDrugBillNo"></param>
        /// <returns></returns>
        string ReturnDrugSingleThread(tyInfo tyInfo, out string returnDrugBillNo);

        /// <summary>
        /// Lấy đơn thuốc hiệu lực theo số đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> SelectRpList(string cfh, string organizeId);

        /// <summary>
        /// Trả thuốc ngoại trú một phần
        /// </summary>
        /// <param name="item"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string OutpatientPartReturn(ReturnItemData item, string organizeId);
    }
}