using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Thông tin số lô chi tiết đơn ngoại trú mz_cfmxph
    /// </summary>
    public interface IOutpatientPrescriptionDetailBatchNumberRepo : IRepositoryBase<OutpatientPrescriptionDetailBatchNumberEntity>
    {
        /// <summary>
        /// Lấy tất cả thông tin số lô chi tiết đơn ngoại trú
        /// </summary>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList();

        /// <summary>
        /// Lấy thông tin số lô chi tiết đơn ngoại trú chưa xếp kệ theo số đơn thuốc và tổ chức
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="gjzt">Trạng thái xếp kệ：0-Chưa xếp lên kệ  1-Đã xếp lên kệ</param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string organizeId, string gjzt = "0");

        /// <summary>
        /// Lấy thông tin số lô chi tiết đơn ngoại trú theo số đơn thuốc và tổ chức
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gjzt">Trạng thái xếp kệ：0-Chưa xếp lên kệ  1-Đã xếp lên kệ</param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string yfbmCode, string organizeId, string gjzt = "0");

        /// <summary>
        /// Thuốc xếp kệ
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
