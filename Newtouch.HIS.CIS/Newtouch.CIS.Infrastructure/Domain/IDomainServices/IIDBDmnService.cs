using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO;
using Newtouch.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// tronggiữakholiên quan
    /// </summary>
    public interface IIDBDmnService
    {
        /// <summary>
        /// Tra cứu Bệnh nhânđăng ký khámthông tin
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh">//Lọcchuyên giasố chỉ xemtreotựtựsốcủa</param>
        /// <param name="mjzbz">//1thông thường Ngoại trú 2gấpkhám 3chuyên gia Ngoại trú</param>
        /// <param name="jiuzhenbz"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<TreatmentEntity> GetRegistrationInfoList(Pagination pagination, string orgId, string ksCode, string ysgh, string mjzbz, int jiuzhenbz, string keyword);

        /// <summary>
        /// sẽmzhcủa Chưa thu phí Đơn thuốc+Khám bệnh Cờ đợithông tin Đồng bộđếntronggiữakho
        /// (và Đồng bộ Đơn thuốcthành côngsaucầnvềviết Đơn thuốc Trạng thái đồng bộ)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        void SyncTo(string orgId, string mzh, string cfId = null);

        /// <summary>
        /// Hủy bỏhis Một lần Khám bệnhcủatất cả Đơn thuốc
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="orgId"></param>
        void ObsoleteAllPresToHIS(string jzId, string orgId);

        /// <summary>
        /// Hủy bỏ HIS phiếu tờ Đơn thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        /// <param name="apicflx"></param>
        /// <param name="apicfh"></param>
        void cancelSinglePresToHIS(string orgId, string mzh, string cfId
            , int apicflx, string apicfh);


        /// <summary>
        /// CIS Đơn thuốc Dữ liệu
        /// </summary>
        /// <param name="Opt"></param>
        /// <param name="Brwym"></param>
        /// <param name="Zdlx"></param>
        /// <param name="Brxz"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
         List<PrescriptionDto> GetCisPrescription(string Opt, string Brwym, string Zdlx, string Brxz, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// tronggiữakho Đồng bộ Đơn thuốc Thu phí Trạng thái
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
         void Refreshcfsfbz(string blh, string orgId);
    }
}
