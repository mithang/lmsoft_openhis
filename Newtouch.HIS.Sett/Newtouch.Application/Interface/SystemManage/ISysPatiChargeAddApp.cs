using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatiChargeAddApp
    {
        //DataTable GetList(int keyValue);

        List<SysPatiChargeAddVo> GetSysPatiChargeAddVoList(string keyword, int? bh = null);
        void DeleteForm(SysPatientChargeAdditionalEntity sysPatiChargeAddEntity);
        void SubmitForm(SysPatientChargeAdditionalEntity sysPatiChargeAddEntity, int? keyValue);

        List<SysChargeAdditionalCategoryEntity> GetfjsfdlList(string keyValue);
        /// <summary>
        /// Lấy phí dịch vụ tỷ lệ 
        /// </summary>
        /// <param name="brxz"></param>
        /// <param name="sfxm"></param>
        /// <returns></returns>
        decimal? GetFWFBL(string brxz, string dl, string sfxm, decimal dj);
    }
}
