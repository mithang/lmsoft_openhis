using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.CIS.API.Envelope;

namespace Newtouch.CIS.API.Requests
{
    /// <summary>
    /// DTO của NewtouchCIS.API gốc, port nguyên trường lên RequestBase của envelope
    /// (DTO gốc trong Infrastructure kế thừa RequestBase rỗng — chỉ để Web dùng nội bộ,
    /// không mang được Token/Timestamp/AppId nên API định nghĩa lại tại đây).
    /// </summary>

    /// <summary>Request cấp token gốc — KHÔNG còn dùng để cấp token thật (xem AuthController).</summary>
    public class AuthGetTokenRequest : RequestBase
    {
        public string UserId { get; set; }
        public string Account { get; set; }
        public string OrganizeId { get; set; }
        public string TopOrganizeId { get; set; }
    }

    /// <summary>Đổi access_token lấy Account + OrganizeId (UA).</summary>
    public class UAGetUserInfoRequest : RequestBase
    {
        public string access_token { get; set; }
    }

    /// <summary>Nhiệm vụ tổng quát từ MQ (InsideGeneral).</summary>
    public class MqGeneralTaskRequestDto : RequestBase
    {
        public string body { get; set; }
    }

    /// <summary>Kết quả LIS đẩy về (CQKRLIs) — stub không xử lý, chỉ giữ dạng request.</summary>
    public class LISResultDTO : RequestBase
    {
        public string type { get; set; }
        public string Ado_lis { get; set; }
        public string brxx_id { get; set; }
        public string brxx_tmh { get; set; }
        public string zydj_id { get; set; }
    }

    /// <summary>Phiếu đề nghị của khoa (HospitalizedBusiness).</summary>
    public class DeptApplyNoRequest : RequestBase
    {
        public string SqdArray { get; set; }
        public string OrganizeId { get; set; }
        public string UserCode { set; get; }
    }

    /// <summary>Cập nhật thông tin cơ bản bệnh nhân nội trú (PatInfo).</summary>
    public class PatInfoRequest : JSONRequestBase
    {
        /// <summary>Số nhập viện.</summary>
        [Required]
        public string zyh { get; set; }
        /// <summary>Dấu hiệu tại viện.</summary>
        public string zybz { get; set; }
        /// <summary>Chẩn đoán nhập viện.</summary>
        public string ryzd { get; set; }
        public string ryzdmc { get; set; }
        /// <summary>Mã tính chất bệnh nhân.</summary>
        public string brxz { get; set; }
        /// <summary>Tên tính chất bệnh nhân.</summary>
        public string brxzmc { get; set; }
        /// <summary>Tên người liên hệ.</summary>
        public string lxr { get; set; }
        /// <summary>Quan hệ người liên hệ.</summary>
        public string lxrgx { get; set; }
        /// <summary>Điện thoại người liên hệ.</summary>
        public string lxrdh { get; set; }
    }

    /// <summary>Cập nhật tính chất bệnh nhân (PatInfo).</summary>
    public class UpdatebrxzRequest : JSONRequestBase
    {
        public string zyh { get; set; }
        public string mzh { get; set; }
        [Required]
        public string brxzCode { get; set; }
        public string brxzmc { get; set; }
    }

    /// <summary>Lấy thông tin cơ bản bệnh nhân nội trú (PatInfo).</summary>
    public class InpatientBaseInfoRequest : JSONRequestBase
    {
        /// <summary>Số nhập viện.</summary>
        public string zyh { get; set; }
        /// <summary>Họ tên bệnh nhân.</summary>
        public string xm { get; set; }
        /// <summary>ID tổ chức.</summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>Tra cứu thông tin khám bệnh của bệnh nhân (PatientTreatmentInfo).</summary>
    public class PatientTreatmentInfoQueryRequestDTO : RequestBase
    {
        /// <summary>Số khám ngoại trú.</summary>
        public string mzh { get; set; }
        /// <summary>ID tổ chức.</summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>Tra cứu đặt hẹn khám ngoại trú (MzBespeakRegister).</summary>
    public class MzBespeakRegisterQueryRequestDTO : RequestBase
    {
        /// <summary>Loại giấy tờ.</summary>
        public int? zjlx { get; set; }
        /// <summary>Số giấy tờ.</summary>
        [Required]
        public string zjh { get; set; }
        /// <summary>Số bệnh án.</summary>
        [Required]
        public string blh { get; set; }
        /// <summary>Số thẻ.</summary>
        public string kh { get; set; }
    }

    /// <summary>Tra cứu lịch trực đặt hẹn của khoa/chuyên gia (MzBespeakRegister).</summary>
    public class SysBespeakRegisterQueryRequestDTO : RequestBase
    {
        /// <summary>Mã khoa.</summary>
        [Required]
        public string ksCode { get; set; }
        /// <summary>Mã công tác bác sĩ.</summary>
        public string ysgh { get; set; }
        /// <summary>Ngày đặt hẹn.</summary>
        public DateTime regDate { get; set; }
        /// <summary>Khung giờ đặt hẹn.</summary>
        public string regTime { get; set; }
    }

    /// <summary>Đếm số đã đặt hẹn của khoa/chuyên gia (MzBespeakRegister).</summary>
    public class MzAlreadyBespeakRegisterCountQueryRequestDTO : RequestBase
    {
        /// <summary>Mã khoa.</summary>
        public string ksCode { get; set; }
        /// <summary>Mã công tác bác sĩ.</summary>
        public string ysgh { get; set; }
        /// <summary>Ngày đặt hẹn.</summary>
        public DateTime regDate { get; set; }
        /// <summary>Thời gian đặt hẹn.</summary>
        public string regTime { get; set; }
    }

    /// <summary>Xác nhận đến khám theo hẹn (MzBespeakRegister) — ghi dữ liệu.</summary>
    public class KeepAnAppointmentRequestDTO : RequestBase
    {
        /// <summary>Khóa chính đặt hẹn mz_yygh.id.</summary>
        public string mzyyghId { get; set; }
        /// <summary>Thời gian đến khám.</summary>
        public DateTime arrivalDate { get; set; }
        /// <summary>Nhân viên xử lý tại chỗ.</summary>
        public string arrivalOpereater { get; set; }
    }

    /// <summary>Một dòng đơn thuốc trong request cập nhật trạng thái thu phí.</summary>
    public class PrescriptionChargeDto
    {
        /// <summary>Số đơn thuốc.</summary>
        [Required]
        public string cfh { get; set; }
    }

    /// <summary>Cập nhật dấu hiệu thu phí đơn thuốc (Prescription).</summary>
    public class PrescriptionChargeRequest : RequestBase
    {
        /// <summary>Danh sách số đơn thuốc.</summary>
        public List<PrescriptionChargeDto> cfList { get; set; }
        /// <summary>Dấu hiệu thu phí.</summary>
        [Required]
        public bool sfbz { get; set; }
        public string OrganizeId { get; set; } = null;
    }

    /// <summary>Cập nhật dấu hiệu trả phí đơn thuốc (Prescription).</summary>
    public class PrescriptionReturnChargeRequest : RequestBase
    {
        /// <summary>Danh sách số đơn thuốc.</summary>
        public List<string> cfList { get; set; }
        /// <summary>Dấu hiệu trả lại.</summary>
        [Required]
        public bool tbz { get; set; }
    }

    /// <summary>Gọi duyệt bệnh án thông minh (Review).</summary>
    public class ReviewRequest : RequestBase
    {
        public string orgId { get; set; }
        public string zyh { get; set; }
        public string rygh { get; set; }
        public string username { get; set; }
        public string GetMAC { get; set; }
    }
}
