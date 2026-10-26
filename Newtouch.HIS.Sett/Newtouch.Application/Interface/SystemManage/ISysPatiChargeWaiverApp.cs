using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatiChargeWaiverApp
    {
        List<SysPatiChargeWaiverVo> GetEffectiveList(string keyValue, int? bh = null);

        void DeleteForm(int keyValue);
        void SubmitForm(SysPatientChargeWaiverEntity sysPatiChargeWaiverEntity, int? keyValue);

        /// <summary>
        /// Tính hạng mục thu phí số tiền giảm trừ
        /// Bệnh nhânTrạng thái:1;--Hiệu lực
        /// thay đ ổi dấu hiệu:0;--chưa thay đ ổi 
        /// </summary>
        /// <param name="parmBrxz">Tính chất bệnh nhân</param>
        /// <param name="parmDl"> loại lớn </param>
        /// <param name="parmSfxm">Hạng mục thu phí</param>
        /// <param name="parmJe">Số tiền</param>
        /// <param name="outJmbl">Tỷ lệ giảm trừ</param>
        /// <param name="outJmje">Số tiền giảm trừ</param>
        /// <returns>count:Số tiền sau giảm trừ</returns>
        decimal Calcjm(string parmBrxz, string parmDl, string parmSfxm, decimal parmJe, out decimal outJmbl, out decimal outJmje);

    }
}
