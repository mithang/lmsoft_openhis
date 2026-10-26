using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookkeepInpatientApp
    {
        /// <summary>
        /// theo số nhập viện lấy bệnh nhân ghi sổ thông tin
        /// </summary>
        /// <param name="zyh"></param>
        HosAccountingPatStatusDetailDto GetAccountingStatusDetail(string zyh);

        /// <summary>
        /// GửiKế hoạch ghi sổ
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="xmList">Ghi sổ hạng mụcdanh sách(nếu là nhưng hạng mục, cũng cần trư ớc đóng gói thành nhóm)</param>
        void SubmitAccountingPlan(string zyh, IList<InpatientAccountingPlanItemDto> xmList);

        /// <summary>
        /// chờ thực hiện kế hoạch ghi sổ tra cứu
        /// </summary>
        /// <param name="zyhList"></param>
        /// <param name="zxrq"></param>
        IList<HospAccountingPlanWaitingExeVO> WaitingAccountingPlanQuery(IList<string> zyhList,string from = null);

        /// <summary>
        /// thực hiện kế hoạch ghi sổ
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxIdDanh sách</param>
        void ExecuteAccountingPlan(IList<zxGirdDto> zxItem, DateTime? zxrq, string from = null);

        /// <summary>
        /// dừng kế hoạch ghi sổ
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxIdDanh sách</param>
        void StopAccountingPlan(string jzjhmxIdStr, DateTime stopDate);

        /// <summary>
        /// Hủydự kiến dừng 
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxIdDanh sách</param>
        void CancelPreStopAccountingPlan(string jzjhmxIdStr);

        /// <summary>
        /// theo số nhập viện lấy bệnh nhân ghi sổ thông tin
        /// </summary>
        /// <param name="zyh"></param>
        InpatientHosAccountingPatStatusDetailDto GetInpatientAccountingStatusDetail(string zyh);
    }
}
