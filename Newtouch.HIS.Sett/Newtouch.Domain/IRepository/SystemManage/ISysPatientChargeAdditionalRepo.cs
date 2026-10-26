using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysPatientChargeAdditionalRepo : IRepositoryBase<SysPatientChargeAdditionalEntity>
    {
        void DeleteForm(SysPatientChargeAdditionalEntity sysPatiChargeAddEntity, string orgId);
        void SubmitForm(SysPatientChargeAdditionalEntity sysPatiChargeAddEntity, int? keyValue, string orgId);

        /// <summary>
        /// Lấy tỷ lệ phí dịch vụ
        /// </summary>
        /// <param name="brxz">Tính chất bệnh nhân</param>
        /// <param name="dl">Loại lớn</param>
        /// <param name="sfxm">Hạng mục thu phí</param>
        /// <returns></returns>
        SysPatientChargeAdditionalEntity GetFWFBL(string brxz, string dl, string sfxm, string orgId);

        List<SysPatientChargeAdditionalEntity> SelectALLEffectiveList(string orgId);
    }
}
