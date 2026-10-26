using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Proxy.guian.DTO;
using System;

namespace Newtouch.HIS.Application.Interface.HospitalizationManage
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDischargeSettleApp
    {

        #region 出院结算

        /// <summary>
        /// Số nhập việntra cứu dữ liệu(bao gồm thông tin bệnh nhân và tính phí chi tiết) zyh hoặc kh +cardType hoặc sfz +cardType
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="sfz"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        InpatientSettPatStatusDetailDto GetInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx,string ver);

        /// <summary>
        /// LưuQuyết toán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedcyrq"></param>
        /// <param name="fph"></param>
        /// <param name="feeRelated">Số tiền và thanh toán thông tin</param>
        /// <param name="ybfeeRelated">BHYT liên quan chi phí thông tin</param>
        void SaveSett(string zyh, DateTime expectedcyrq, string fph, InpatientSettFeeRelatedDTO feeRelated
            , CQZyjs05Dto ybfeeRelated, S14OutResponseDTO xnhfeeRelated
            , string outTradeNo, string jslx, out int jsnm);

        InpatientSettPatStatusDetailDto GetZyUploadDetail(string zyh,string jslx);

        InpatIentFeeSumVo GetInpatientSettleJe(string zyh, string sczt, DateTime? kssj, DateTime jssj, string xmmc);
        #endregion

        #region 取消出院结算
        /// <summary>
        /// Tra cứu
        /// </summary>
        InpatientCancelSettPatStatusDetailDto GetCancelSettleStatusDetail(string zyh, string sfz, string kh, string cardType);

        /// <summary>
        /// HủyQuyết toán xuất viện
        /// </summary>
        void DoCancel(string zyh, int expectedjsnm, string cancelReason,string cancelyblsh, out string outTradeNo, out decimal refundAmount);

        #endregion

        #region 模拟结算（GA）



        #endregion


        #region 中途结算
        void SavePartialSettle(string zyh, DateTime startTime, DateTime endTime, string fph, InpatientSettFeeRelatedDTO feeRelated
           , CQZyjs05Dto ybfeeRelated, string outTradeNo, string jslx, out int jsnm);

        InpatientSettPatStatusDetailDto GetPartialInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx,string ver);

        /// <summary>
        /// chuyển quyết toán xuất viện tiền xử lý
        /// </summary>
        /// <param name="zyh"></param>
         string PreCYsettle(string zyh);
        #endregion
    }
}
