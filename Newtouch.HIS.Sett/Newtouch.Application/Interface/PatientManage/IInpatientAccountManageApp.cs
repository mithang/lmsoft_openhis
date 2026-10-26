using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.Core.Common;
using System.Threading.Tasks;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;

namespace Newtouch.HIS.Application.Interface.PatientManage
{
    public interface IInpatientAccountManageApp
    {
        InpatientReserveDto GetHosPatAccInfo(string zyh);
        InpatientReserveDto GetHosPatAccInfo(string zyh, string zhxz);
        /// <summary>
        /// Nạp tiền
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="szid"></param>
        /// <returns></returns>
        bool PayDepositPost(DeposDto dto, out string szid);
        /// <summary>
        /// lấy khoản 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool ReturnDepositPost(DeposDto dto);
        /// <summary>
        /// số dư khuyên trả lại 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool RefundAccount(DeposDto dto);
        /// <summary>
        /// Lấy số dư tài khoản
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        decimal GetzhYe(string zyh);

        /// <summary>
        /// Lấy bệnh nhânthu chi tài khoản bản ghi 
        /// </summary>
        /// <returns></returns>
        List<InpatientPatAccPayVO> GetPayList(int zh, string zhxz);
        string GetFinRepSJPZH();

    }
}
