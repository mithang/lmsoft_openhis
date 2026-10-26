using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{

    /// <summary>
    /// 
    /// </summary>
    public interface ISysRegistSpecialDiseaseRepo : IRepositoryBase<SysRegistSpecialDiseaseEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ghzb"></param>
        /// <returns></returns>
        SysRegistSpecialDiseaseEntity SelectSysChargeItemByghzb(string ghzb, string orgId);

        /// <summary>
        /// Lấy tất cả bệnh chuyên khoa đăng ký khám
        /// </summary>
        /// <param name="ghzb"></param>
        /// <returns></returns>
        List<SysRegistSpecialDiseaseEntity> SelectSysChargeItemByghzbList(string orgId);
    }
}
