using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Đơn ngoại trú
    /// </summary>
    public interface IMzCfmxRepo : IRepositoryBase<MzCfmxEntity>
    {
        /// <summary>
        /// Lấy chi tiết đơn thuốc theo số đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        List<MzCfmxEntity> GetCfmxByCfh(string cfh);

        /// <summary>
        /// Kiểm tra đã tồn tại chưa
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gg">Quy cách</param>
        /// <returns></returns>
        bool IsExist(string cfh, string ypCode, string organizeId, string gg = "");

        /// <summary>
        /// Xóa chi tiết đơn thuốc cũ
        /// </summary>
        /// <param name="cfh">Số đơn thuốc</param>
        /// <returns></returns>
        int DeleteCfmx(string cfh);

        /// <summary>
        /// Xóa chi tiết đơn thuốc cũ
        /// </summary>
        /// <param name="cfhs">Danh sách số đơn thuốc</param>
        /// <returns></returns>
        int DeleteCfmx(List<string> cfhs);
    }
}