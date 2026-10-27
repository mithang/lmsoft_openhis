using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Thực hiện y lệnh thuốc nội trú
    /// </summary>
    public interface IZyYpyzxxRepo : IRepositoryBase<ZyYpyzxxEntity>
    {

        /// <summary>
        /// Lấy khu bệnh cần phát thuốc
        /// </summary>
        /// <returns></returns>
        List<string> GetFyBq();

        /// <summary>
        /// Lấy bệnh nhân cần phát thuốc
        /// </summary>
        /// <returns></returns>
        List<string> GetFyPatients();

        /// <summary>
        /// Lấy thông tin y lệnh no log
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string organizeId);

        /// <summary>
        /// Lấy thông tin y lệnh no log
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="zxId">Thực hiện y lệnhID</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string zxId, string organizeId);

        /// <summary>
        /// Lấy thông tin y lệnh no log
        /// </summary>
        /// <param name="yzId">Y lệnhID</param>
        /// <param name="zxId">Thực hiện y lệnhID</param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string zxId, string ypCode, string organizeId);

        /// <summary>
        /// Sửa cờ phát thuốc thành đã trả thuốc
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="db"></param>
        int UpdateFybzTy(string yzId, string zxId, string userCode, string organizeId, Newtouch.Infrastructure.EFDbTransaction db);

        /// <summary>
        /// Theo thực hiệnID，Xóa vật lý tất cả chi tiết của lần thực hiện này
        /// </summary>
        /// <param name="zxId"></param>
        /// <returns></returns>
        int DeleteByZxId(string zxId);
    }
}