using Newtouch.Core.Common;
using Newtouch.Domain.DTO;
using Newtouch.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Liên quan tới CSDL trung gian
    /// </summary>
    public interface IIDBDmnService
    {
        /// <summary>
        /// Tra cứu thông tin đăng ký khám bệnh nhân
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh">//Lọc số chuyên gia Chỉ xem số đăng ký của chính mình</param>
        /// <param name="mjzbz">//1Khám thường ngoại trú 2Cấp cứu 3Khám chuyên gia</param>
        /// <param name="jiuzhenbz"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<TreatmentEntity> GetRegistrationInfoList(Pagination pagination, string orgId, string ksCode, string ysgh, string mjzbz, int jiuzhenbz, string keyword);

        /// <summary>
        /// sẽmzhđơn thuốc chưa thu phí của+Cờ khám bệnh và các thông tin khác Đồng bộ tới CSDL trung gian
        /// (Và sau khi đồng bộ đơn thuốc thành công phải ghi lại trạng thái đồng bộ đơn thuốc)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        void SyncTo(string orgId, string mzh, string cfId = null);

        /// <summary>
        /// Hủy bỏhisTất cả đơn thuốc của một lần khám
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="orgId"></param>
        void ObsoleteAllPresToHIS(string jzId, string orgId);

        /// <summary>
        /// Hủy bỏHISMột đơn thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        /// <param name="apicflx"></param>
        /// <param name="apicfh"></param>
        void cancelSinglePresToHIS(string orgId, string mzh, string cfId
            , int apicflx, string apicfh);


        /// <summary>
        /// CISDữ liệu đơn thuốc
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
        /// CSDL trung gian Đồng bộ trạng thái thu phí đơn thuốc
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
         void Refreshcfsfbz(string blh, string orgId);
    }
}
