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
        /// Tra cứuBệnh nhânđăng ký khámthông tin
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh">//Lọcchuyên giasố chỉ xemtreotựtựsốcủa</param>
        /// <param name="mjzbz">//1thông thườngNgoại trú 2gấpkhám 3chuyên giaNgoại trú</param>
        /// <param name="jiuzhenbz"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<TreatmentEntity> GetRegistrationInfoList(Pagination pagination, string orgId, string ksCode, string ysgh, string mjzbz, int jiuzhenbz, string keyword);

        /// <summary>
        /// sẽmzhcủaChưa thu phíĐơn thuốc+Khám bệnhCờ đợithông tin Đồng bộđếntronggiữakho
        /// （vàĐồng bộĐơn thuốcthành côngsaucầnvềviếtĐơn thuốcTrạng thái đồng bộ）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        void SyncTo(string orgId, string mzh, string cfId = null);

        /// <summary>
        /// Hủy bỏhisMột lầnKhám bệnhcủatất cảĐơn thuốc
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="orgId"></param>
        void ObsoleteAllPresToHIS(string jzId, string orgId);

        /// <summary>
        /// Hủy bỏHISphiếutờĐơn thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        /// <param name="apicflx"></param>
        /// <param name="apicfh"></param>
        void cancelSinglePresToHIS(string orgId, string mzh, string cfId
            , int apicflx, string apicfh);


        /// <summary>
        /// CISĐơn thuốcDữ liệu
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
        /// tronggiữakho Đồng bộĐơn thuốcThu phíTrạng thái
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
         void Refreshcfsfbz(string blh, string orgId);
    }
}
