using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// ngoại trú chi tiết đơn thuốc số lô thông tin mz_cfmxph
    /// </summary>
    public interface IOutpatientPrescriptionDetailBatchNumberRepo : IRepositoryBase<OutpatientPrescriptionDetailBatchNumberEntity>
    {
        /// <summary>
        /// lấy tất cả ngoại trú chi tiết đơn thuốc số lô thông tin
        /// </summary>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList();

        /// <summary>
        /// theo số đơn thuốc và tổ chức lấy chưa xếp lên kệ của ngoại trú chi tiết đơn thuốc số lô thông tin
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="gjzt">xếp lên kệ trạng thái: 0-chưa xếp lên kệ  1-đã xếp lên kệ</param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string organizeId, string gjzt = "0");

        /// <summary>
        /// theo số đơn thuốc và tổ chức lấy ngoại trú chi tiết đơn thuốc số lô thông tin
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gjzt">xếp lên kệ trạng thái: 0-chưa xếp lên kệ  1-đã xếp lên kệ</param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string yfbmCode, string organizeId, string gjzt = "0");

        /// <summary>
        /// xếp lên kệ thuốc
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int GJDrug(string ypCode, string pc, string ph, string cfh, string yfbmCode, string organizeId,
           string userCode);
    }
}
