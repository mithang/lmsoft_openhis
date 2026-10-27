using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Số lô thực hiện y lệnh thuốc nội trú
    /// </summary>
    public interface IZyYpyzzxphRepo : IRepositoryBase<ZyYpyzzxphEntity>
    {
        /// <summary>
        /// Lấy thông tin xếp thuốc nội trú hiệu lực chưa xếp kệ  Phương thức này không ghi nhật ký
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="zxId">Thực hiệnId</param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzzxphEntity> SelectZyphList(string yzId, string zxId, string ypCode, string organizeId);

        /// <summary>
        /// Lấy thông tin xếp thuốc nội trú hiệu lực chưa xếp kệ  Phương thức này không ghi nhật ký
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="organizeId"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        List<ZyYpyzzxphEntity> SelectZyphList(string yzId, string zxId, string ypCode, string pc, string ph, string organizeId);
    }
}