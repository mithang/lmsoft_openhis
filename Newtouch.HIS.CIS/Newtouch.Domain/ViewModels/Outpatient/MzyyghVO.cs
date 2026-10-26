using System;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    /// <summary>
    /// Thông tin đăng ký khám theo hẹn
    /// </summary>
    public class MzyyghVO
    {
        /// <summary>
        /// Đăng ký khám theo hẹnID mz_yyghKhóa chính của bảng
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Cơ cấu tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Loại ngoại trú  1:Khám thường ngoại trú    2:Cấp cứu     3:Khám chuyên gia
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// Ngày hẹn
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// Khung giờ
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Mã nhân viên chuyên gia
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// Tên chuyên gia
        /// </summary>
        public string zjmc { get; set; }

        /// <summary>
        /// Số hẹn
        /// </summary>
        public int bespeakNo { get; set; }

        /// <summary>
        /// Thời gian gửi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người gửi
        /// </summary>
        public string CreatorCode { get; set; }
    }

    /// <summary>
    /// Chi tiết đăng ký khám hẹn ngoại trú
    /// </summary>
    public class MzyyghDetailVo
    {
        /// <summary>
        /// Số lượng hẹn tối đa được hỗ trợ
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// Số đã hẹn
        /// </summary>
        public int bespeakedCount { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string departmentCode { get; set; }

        /// <summary>
        /// Mã nhân viên chuyên gia
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Ngày hẹn
        /// </summary>
        public DateTime regDate { get; set; }
    }

    /// <summary>
    /// Lịch trực đăng ký khám theo hẹn
    /// </summary>
    public class BespeakRegisterScheduling
    {
        /// <summary>
        /// Ngày lịch
        /// </summary>
        public DateTime CalendarDate { get; set; }

        public MzyyghDetailVo schedulingData { get; set; }
    }
}
