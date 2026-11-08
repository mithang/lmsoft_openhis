using System;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    /// <summary>
    /// đặt hẹn đăng ký khám thông tin
    /// </summary>
    public class MzyyghVO
    {
        /// <summary>
        /// đặt hẹn đăng ký khám ID mz_yyghbảng Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chức kết cấu trúc ID
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
        /// khi đoạn
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
        /// đặt hẹn số
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
    /// Ngoại trú đặt hẹn đăng ký khám chi tiết
    /// </summary>
    public class MzyyghDetailVo
    {
        /// <summary>
        /// nhất lớn Hỗ trợ đặt hẹn số
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// đã đặt hẹn số
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
    /// đăng ký khám đặt hẹn Xếp lịch
    /// </summary>
    public class BespeakRegisterScheduling
    {
        /// <summary>
        /// ngày lịch Ngày
        /// </summary>
        public DateTime CalendarDate { get; set; }

        public MzyyghDetailVo schedulingData { get; set; }
    }
}
