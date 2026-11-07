using System;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    /// <summary>
    /// đặt hẹnđăng ký khámthông tin
    /// </summary>
    public class MzyyghVO
    {
        /// <summary>
        /// đặt hẹnđăng ký khám ID mz_yyghbảng Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúc ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khoa Mã
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Ngoại trú Loại  1:thông thường Ngoại trú    2:Cấp cứu     3:chuyên gia Ngoại trú
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// đặt hẹn Ngày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// khiđoạn
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// chuyên gia Mã nhân viên
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// chuyên gia Tên
        /// </summary>
        public string zjmc { get; set; }

        /// <summary>
        /// đặt hẹnsố
        /// </summary>
        public int bespeakNo { get; set; }

        /// <summary>
        /// Gửi Thời gian
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gửi người
        /// </summary>
        public string CreatorCode { get; set; }
    }

    /// <summary>
    /// Ngoại trúđặt hẹnđăng ký khámchi tiết
    /// </summary>
    public class MzyyghDetailVo
    {
        /// <summary>
        /// nhấtlớn Hỗ trợđặt hẹnsố
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// đãđặt hẹnsố
        /// </summary>
        public int bespeakedCount { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string departmentCode { get; set; }

        /// <summary>
        /// chuyên gia Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// đặt hẹn Ngày
        /// </summary>
        public DateTime regDate { get; set; }
    }

    /// <summary>
    /// đăng ký khámđặt hẹn Xếp lịch
    /// </summary>
    public class BespeakRegisterScheduling
    {
        /// <summary>
        /// ngàylịch Ngày
        /// </summary>
        public DateTime CalendarDate { get; set; }

        public MzyyghDetailVo schedulingData { get; set; }
    }
}
