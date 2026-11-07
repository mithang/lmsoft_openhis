using System;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// Ngoại trúđặt hẹn Tra cứu Quay lạibáovăn
    /// </summary>
    public class MzBespeakRegisterQueryResponseDTO
    {
        /// <summary>
        /// Ngoại trúđặt hẹnđăng ký khám ID
        /// </summary>
        public string mzyyghId { get; set; }
        
        /// <summary>
        /// chứngmục Loại
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        
        /// <summary>
        /// Dấu hiệu ngoại trú/nội trú 1:thông thường Ngoại trú  2:gấpkhám   3:chuyên gia Ngoại trú
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// đăng ký khám Ngày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// đăng ký khám Thời gian
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Khoa Mã
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Bác sĩ Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Bác sĩ Tên
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// nhấtlớnđặt hẹnsố
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// đặt hẹnsố
        /// </summary>
        public int bespeakNo { get; set; }

        /// <summary>
        /// Thời gian đến hẹn
        /// </summary>
        public DateTime? arrivalDate { get; set; }

        /// <summary>
        /// hiệntrườngchỗlýđăng ký khám ngườiviên
        /// </summary>
        public string arrivalOpereater { get; set; }

        /// <summary>
        /// đặt hẹn Thao tác Thời gian
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// đặt hẹn Thao tác ngườiviên
        /// </summary>
        public string CreatorCode { get; set; }
    }
}
