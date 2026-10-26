using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// nội trú thuốc thực hiện y lệnh
    /// </summary>
    public interface IZyYpyzxxRepo : IRepositoryBase<ZyYpyzxxEntity>
    {

        /// <summary>
        /// lấy cần phát thuốc của khu bệnh
        /// </summary>
        /// <returns></returns>
        List<string> GetFyBq();

        /// <summary>
        /// lấy cần phát thuốc của bệnh nhân
        /// </summary>
        /// <returns></returns>
        List<string> GetFyPatients();

        /// <summary>
        /// lấy y lệnh thông tin no log
        /// </summary>
        /// <param name="yzId">y lệnh ID</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string organizeId);

        /// <summary>
        /// lấy y lệnh thông tin no log
        /// </summary>
        /// <param name="yzId">y lệnh ID</param>
        /// <param name="zxId">thực hiện y lệnh ID</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string zxId, string organizeId);

        /// <summary>
        /// lấy y lệnh thông tin no log
        /// </summary>
        /// <param name="yzId">y lệnh ID</param>
        /// <param name="zxId">thực hiện y lệnh ID</param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string zxId, string ypCode, string organizeId);

        /// <summary>
        /// sửa cờ phát thuốc vị đã trả thuốc
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="db"></param>
        int UpdateFybzTy(string yzId, string zxId, string userCode, string organizeId, EFDbTransaction db);

        /// <summary>
        /// theo thực hiện ID, xóa vật lý tất cả chi tiết của lần thực hiện này
        /// </summary>
        /// <param name="zxId"></param>
        /// <returns></returns>
        int DeleteByZxId(string zxId);
    }
}