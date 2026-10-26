using FrameworkBase.MultiOrg.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Loại lớn thu phí
    /// </summary>
    public interface ISysChargeCategoryRepo : IRepositoryBase<SysChargeCategoryVEntity>
    {
        /// <summary>
        /// Lấy danh sách loại lớn thu phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dllbs"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<SysChargeCategoryVEntity> GetList(string orgId, string dllbs = "", string zt = "");

        /// <summary>
        /// Thu tất cả các loại lớn thu phí(Kèm bộ nhớ đệm)
        /// </summary>
        /// <returns></returns>
        IList<SysChargeCategoryVEntity> GetLazyList(string orgId);

    }
}
