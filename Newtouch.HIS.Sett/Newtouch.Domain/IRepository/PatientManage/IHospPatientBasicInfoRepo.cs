using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHospPatientBasicInfoRepo : IRepositoryBase<HospPatientBasicInfoEntity>
    {
        /// <summary>
        /// Lấy thông tin cơ bản nội trú bệnh nhân theo số nhập viện
        /// add by HLF
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="kh">Số thẻ</param> 
        /// <param name="xm">Họ tên</param> 
        /// <returns></returns>
        List<HospPatientBasicInfoEntity> GetHosPatInfoList(string zyh, string kh, string xm, string OrganizeId);

        /// <summary>
        /// Lấy thông tin cơ bản nội trú hiệu lực theo mã nội bộ bệnh nhân
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        HospPatientBasicInfoEntity GetFirstPatiInfoByPatid(int? patid);

        /// <summary>
        /// TheozyhXác định có tồn tại thông tin cơ bản bệnh nhân hay không
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetBlhByZyh(string zyh, string orgId);

        /// <summary>
        /// Lấy danh sách bệnh nhân của cơ sở
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<PendingExecutionPatientVO> GetWardPatientVOListByOrg(string orgId, string bqCode = null);

        /// <summary>
        /// TheozyhLấy thông tin bệnh nhân
        /// </summary>
        HospPatientBasicInfoEntity GetInpatientInfoByZyh(string zyh, string orgId);

        /// <summary>
        /// Lọc bệnh nhân nội trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="zybz"></param>
        /// <returns></returns>
        List<HospPatientBasicInfoEntity> GetInpatientList(string orgId, string keyword = null, string zybz = "zaiyuanbr");

        /// <summary>
        /// Cập nhật cờ đang nằm viện của bệnh nhân nội trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="zybz"></param>
        void UpdateInpatientStatus(string orgId, string zyh, string zybz);
        /// <summary>
        /// Thông qua ra khỏi khu,Khi chuyển khu thì cập nhật thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="bq"></param>
        /// <param name="cw"></param>
        /// <param name="cyrq"></param>
        /// <param name="cyzd"></param>
        /// <param name="cybq"></param>
        /// <param name="doctor"></param>
        /// <param name="ryzd"></param>
        /// <param name="rybq"></param>
        void UpdateInpatientOutInfoRequest(string orgId, string zyh, string zybz, string bq, string cw, DateTime? cyrq, string doctor, string cyzd);
        void UpdateInpatientOutRecallInfoRequest(string orgId, string zyh, string zybz, string bq, string cw, DateTime? cyrq, string doctor, string cyzd);
    }
}
