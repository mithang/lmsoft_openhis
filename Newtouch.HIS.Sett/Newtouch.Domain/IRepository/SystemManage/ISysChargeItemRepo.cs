/*******************************************************************************
 * Copyright © 2016 Newtouch Bảo lưu bản quyền
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Hạng mục thu phí
    /// </summary>
    public interface ISysChargeItemRepo : IRepositoryBase<SysChargeItemVEntity>
    {
        /// <summary>
        /// Theosfxm CodeLấyEntity
        /// </summary>
        /// <param name="sfxm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysChargeItemVEntity SelectSysChargeItemBysfxm(string sfxm, string orgId);

        /// <summary>
        /// Lấy theo mãdl
        /// </summary>
        /// <param name="sfxm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetdlCodeBysfxm(string sfxm, string orgId);
    }
}
