using System;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    /// <summary>
    /// đặt hẹnđăng ký khámthông tin
    /// </summary>
    public class MzyyghVO
    {
        /// <summary>
        /// đặt hẹnđăng ký khámID mz_yyghbảngKhóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// KhoaMã
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Ngoại trúLoại  1:thông thườngNgoại trú    2:Cấp cứu     3:chuyên giaNgoại trú
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// đặt hẹnNgày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// khiđoạn
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// chuyên giaMã nhân viên
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// chuyên giaTên
        /// </summary>
        public string zjmc { get; set; }

        /// <summary>
        /// đặt hẹnsố
        /// </summary>
        public int bespeakNo { get; set; }

        /// <summary>
        /// GửiThời gian
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
        /// nhấtlớnHỗ trợđặt hẹnsố
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
        /// chuyên giaMã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// đặt hẹnNgày
        /// </summary>
        public DateTime regDate { get; set; }
    }

    /// <summary>
    /// đăng ký khámđặt hẹnXếp lịch
    /// </summary>
    public class BespeakRegisterScheduling
    {
        /// <summary>
        /// ngàylịchNgày
        /// </summary>
        public DateTime CalendarDate { get; set; }

        public MzyyghDetailVo schedulingData { get; set; }
    }
}
