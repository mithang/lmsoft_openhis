using Newtouch.HIS.Domain.ValueObjects.API;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Sett.Request;

namespace Newtouch.HIS.Domain.IDomainServices.API
{
    public interface IBookingDmnService
    {
        /// <summary>
        /// Lấy thông tin khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="ksmc"></param>
        /// <returns></returns>
        IList<SysDepartmentInfo> GetDepartmentInfo(string orgId, string ks, string ksmc);
        /// <summary>
        /// Lấy thông tin nhân viên bác sĩ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <param name="staffName"></param>
        /// <returns></returns>
        IList<SysStaffVO> GetStaffInfo(string orgId, string gh, string staffName);
        /// <summary>
        /// Lấy thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="zjh"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        IList<SysPatInfoVO> GetPatInfo(string orgId, string kh, string zjh, string xm);
        /// <summary>
        /// LấySys_Item
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cateCode"></param>
        /// <returns></returns>
        IList<SysItemDetailVO> SysItemDetail(string orgId, string cateCode);
        /// <summary>
        /// Lấy thông tin thuốc bệnh viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypId"></param>
        /// <param name="ypmc"></param>
        /// <returns></returns>
        IList<SysDrugVO> GetDrugInfo(string orgId, int ypId, string ypmc);
        /// <summary>
        /// Lấy thông tin hạng mục y tế
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sfxmId"></param>
        /// <param name="sfxmCode"></param>
        /// <param name="sfxmmc"></param>
        /// <returns></returns>
        IList<SysMedicalVO> GetMedicalInfo(string orgId, int sfxmId, string sfxmCode, string sfxmmc);
        /// <summary>
        /// Lấy thông tin bệnh ngoại trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zdCode"></param>
        /// <param name="zdmc"></param>
        /// <param name="icd10"></param>
        /// <returns></returns>
        IList<SysDiseaseVO> GetDiseaseInfo(string orgId, string zdCode, string zdmc, string icd10);
        /// <summary>
        /// Lịch trực ngoại trú schedule
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="OutDate"></param>
        /// <param name="czks"></param>
        /// <param name="ysgh"></param>
        /// <param name="RegType"></param>
        /// <param name="ghpbid"></param>
        /// <param name="ScheduId"></param>
        /// <param name="FromOutDate"></param>
        /// <returns></returns>
        IList<MzpbScheduleVO> GetMzpbSchedule(string orgId, string OutDate, string czks, string ysgh, string RegType,
            string ghpbid, string ScheduId, string FromOutDate = null, int pblx = 1);
        /// <summary>
        /// Lấy thông tin đơn thuốc ngoại trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfnm"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        IList<SysRecipeVO> GetRecipeInfo(string orgId, string cfnm, string mzh);
        /// <summary>
        /// Lấy chi tiết đơn thuốc ngoại trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfmxId"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>
        IList<SysRecipeDrugVO> GetRecipeDrugInfo(string orgId, string cfmxId, string cfnm);
        /// <summary>
        /// Lấy chi tiết nguồn số lịch trực
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ScheduId"></param>
        /// <returns></returns>
        IList<MzpbScheduleVO> GetScheduleDetail(string orgId, string ScheduId);
        /// <summary>
        /// Gửi hẹn khám
        /// </summary>
        /// <param name="req"></param>
        /// <param name="patvo"></param>
        /// <param name="pbvo"></param>
        decimal OutbookingApply(BookingRequestDto req,SysPatInfoVO patvo,MzpbScheduleVO pbvo);
        /// <summary>
        /// Tra cứu bản ghi hẹn khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kh"></param>
        /// <param name="lxdh"></param>
        /// <param name="BookId">Số hẹn khám</param>
        /// <param name="yyzt">Trạng thái hẹn khám</param>
        /// <param name="ksrq">Ngày bắt đầu khám hẹn</param>
        /// <param name="jsrq">Ngày kết thúc khám hẹn</param>
        /// <param name="ks"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        IList<MzghBookRecordVO> OutbookRecord(string orgId, string kh, string xm, string lxdh, string BookId, int? yyzt,
            string ksrq, string jsrq, string ks, string ysgh,string appId);

        APIOutputDto OutpatRegApply(string bookId, string kh, string orgId,string payFee,string payLsh,string user);
        APIOutputDto OutPatRegCancel(string orgId, string user, string mzh, string kh, string appId);
        /// <summary>
        /// Đăng ký khám thông thường
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="orgId"></param>
        /// <param name="ghrq"></param>
        /// <param name="ghxz"></param>
        /// <param name="ks"></param>
        /// <param name="user"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        APIOutputDto OutpatReg(string kh, string orgId, string ghrq, string ghxz, string ks, string user, string AppId,string ghlybz);
        /// <summary>
        /// Tra cứu thông tin thu phí bệnh nhân
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kh"></param>
        /// <param name="mzh"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        IList<OutPatientChargeInfoDto> OutpatChargeInfo(string orgId, string kh, string mzh, string AppId);
    }
}
