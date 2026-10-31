using System;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// Ngoại trúđặt hẹnTra cứuQuay lạibáovăn
    /// </summary>
    public class MzBespeakRegisterQueryResponseDTO
    {
        /// <summary>
        /// Ngoại trúđặt hẹnđăng ký khámID
        /// </summary>
        public string mzyyghId { get; set; }
        
        /// <summary>
        /// chứngmụcLoại
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
        /// Dấu hiệu ngoại trú/nội trú 1:thông thườngNgoại trú  2:gấpkhám   3:chuyên giaNgoại trú
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// đăng ký khámNgày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// đăng ký khámThời gian
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// KhoaMã
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Bác sĩMã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Bác sĩTên
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
        /// đặt hẹnThao tácThời gian
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// đặt hẹnThao tác ngườiviên
        /// </summary>
        public string CreatorCode { get; set; }
    }
}
