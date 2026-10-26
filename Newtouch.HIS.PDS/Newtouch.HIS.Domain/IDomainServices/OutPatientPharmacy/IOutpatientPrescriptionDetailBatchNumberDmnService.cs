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
        /// sửa xếp lên kệ cờ
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int UpdateGjztbyCfh(string cfh, string organizeId);

        /// <summary>
        /// theo số đơn thuốc và tổ chức lấy chưa xếp lên kệ của ngoại trú chi tiết đơn thuốc số lô thông tin
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="ypCode"></param>
        /// <param name="gjzt">xếp lên kệ trạng thái: 0-chưa xếp lên kệ  1-đã xếp lên kệ</param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> GetList(string cfh, string organizeId, string ypCode = "", string gjzt = "0");
    }
}
