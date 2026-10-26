using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_bmypxx
    /// </summary>
    public interface ISysPharmacyDepartmentMedicineRepo : IRepositoryBase<SysPharmacyDepartmentMedicineEntity>
    {
        /// <summary>
        /// lấy thuốc vị trí kho
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetYpkw(string ypCode, string yfbmCode, string orgId);

        /// <summary>
        /// bộ phận nhập kho có hay không có quyền khiến dùng nên thuốc
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="rkbm"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        bool CheckRkbmOwnMedicine(string yp, string rkbm, string organizeId);

        /// <summary>
        /// theo thuốc mã và bộ phận nhà thuốc mã lấy này bộ phận thông tin thuốc
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        IList<SysPharmacyDepartmentMedicineEntity> SelectData(string ypCode, string organizeId, string yfbmCode);

        /// <summary>
        /// sửa trạng thái
        /// </summary>
        /// <param name="bmypId"></param>
        /// <param name="zt"></param>
        /// <param name="lastModifierCode"></param>
        /// <param name="lastModifyTime"></param>
        /// <returns></returns>
        int UpdateZt(string bmypId, string zt, string lastModifierCode, DateTime lastModifyTime);

        /// <summary>
        /// sửa trạng thái
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="zt"></param>
        /// <param name="organizeId"></param>
        /// <param name="lastModifierCode"></param>
        /// <param name="lastModifyTime"></param>
        /// <returns></returns>
        int UpdateZt(string ypCode, string zt, string organizeId, string lastModifierCode, DateTime lastModifyTime);

        /// <summary>
        /// sửa trạng thái
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int DeleteItem(string ypCode, string organizeId);

        /// <summary>
        /// sửa trạng thái
        /// </summary>
        /// <param name="bmypId"></param>
        /// <returns></returns>
        int DeleteItem(string bmypId);
    }
}
