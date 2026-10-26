using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Domain.VO;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// trả thuốc
    /// </summary>
    public interface ItyDmnService
    {
        /// <summary>
        /// tra cứu có thể trả thông tin thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RefundedDrugVo> SelectRefundedDrugs(string cfh, string yfbmCode, string organizeId);

        /// <summary>
        /// 查询全退可退药品信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RefundedDrugVo> SelectCompleteRefundedDrugs(string cfh, string yfbmCode, string organizeId);

        /// <summary>
        /// tra cứu thuốc số lượng có thể trả
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns>đơn vị nhỏ nhất số lượng có thể trả</returns>
        int SelectRefundedSl(string ypCode, string pc, string ph, string yfbmCode, string organizeId);

        /// <summary>
        /// trả thuốc ngoại trú
        /// </summary>
        /// <param name="tyInfo"></param>
        /// <param name="returnDrugBillNo"></param>
        /// <returns></returns>
        string ReturnDrug(tyInfo tyInfo, out string returnDrugBillNo);

        /// <summary>
        /// trả thuốc ngoại trú
        /// </summary>
        /// <param name="tyInfo"></param>
        /// <param name="returnDrugBillNo"></param>
        /// <returns></returns>
        string ReturnDrugSingleThread(tyInfo tyInfo, out string returnDrugBillNo);

        /// <summary>
        /// theo số đơn thuốc lấy còn hiệu lực của đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> SelectRpList(string cfh, string organizeId);

        /// <summary>
        /// ngoại trú một phần trả thuốc
        /// </summary>
        /// <param name="item"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string OutpatientPartReturn(ReturnItemData item, string organizeId);
    }
}