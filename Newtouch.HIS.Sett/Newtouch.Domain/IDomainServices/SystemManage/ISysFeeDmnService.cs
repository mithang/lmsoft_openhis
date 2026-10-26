using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Liên quan chi phí hệ thống DmnService
    /// </summary>
    public interface ISysFeeDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfmbbh"></param>
        /// <returns></returns>
        IList<SysChargeItemTemplateVO> GetChargeTemplateChargeItemList(string sfmbbh, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmp"></param>
        /// <param name="itemList"></param>
        void SaveChargeTemplate(SysChargeTemplateEntity tmp, IList<SysChargeItemTemplateVO> itemList);

        /// <summary>
        /// Phạm vi tính phí bệnh nhân hệ thống
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        List<SysPatientChargeRangeVO> GetSysPatientChargeRangeList(string keyValue, int? bh = null);

        /// <summary>
        /// Giảm miễn tính phí bệnh nhân hệ thống
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="bh"></param>
        /// <returns></returns>
        List<SysPatiChargeWaiverVo> GetSysPatiChargeWaiverList(string keyValue, int? bh = null);

        void DeleteById(string keyValue, string orgId);

    }

}
