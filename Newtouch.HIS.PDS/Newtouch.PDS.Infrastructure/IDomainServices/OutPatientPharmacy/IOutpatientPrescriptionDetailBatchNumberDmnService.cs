using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// mz_cfmxph IDmnService
    /// </summary>
    public interface IOutpatientPrescriptionDetailBatchNumberDmnService
    {
        /// <summary>
        /// Sửa cờ xếp kệ
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int UpdateGjztbyCfh(string cfh, string organizeId);

        /// <summary>
        /// Lấy thông tin số lô chi tiết đơn ngoại trú chưa xếp kệ theo số đơn thuốc và tổ chức
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="ypCode"></param>
        /// <param name="gjzt">Trạng thái xếp kệ:0-Chưa xếp lên kệ  1-Đã xếp lên kệ</param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string organizeId, string ypCode = "", string gjzt = "0");
    }
}
