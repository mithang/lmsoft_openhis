using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// nội trú thuốc thực hiện y lệnh số lô
    /// </summary>
    public interface IZyYpyzzxphRepo : IRepositoryBase<ZyYpyzzxphEntity>
    {
        /// <summary>
        /// lấy nội trú còn hiệu lực chưa xếp lên kệ của xếp thuốc thông tin  phương thức này không ghi nhật ký
        /// </summary>
        /// <param name="yzId">y lệnh ID</param>
        /// <param name="zxId">thực hiện Id</param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzzxphEntity> SelectZyphList(string yzId, string zxId, string ypCode, string organizeId);

        /// <summary>
        /// lấy nội trú còn hiệu lực chưa xếp lên kệ của xếp thuốc thông tin  phương thức này không ghi nhật ký
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