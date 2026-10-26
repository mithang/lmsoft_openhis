using System;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// Thông báo trả về khi tra cứu hẹn ngoại trú
    /// </summary>
    public class MzBespeakRegisterQueryResponseDTO
    {
        /// <summary>
        /// Đăng ký khám hẹn ngoại trúID
        /// </summary>
        public string mzyyghId { get; set; }
        
        /// <summary>
        /// Loại giấy tờ
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
        /// Cờ ngoại trú/nội trú 1:Khám thường ngoại trú  2:Cấp cứu   3:Khám chuyên gia
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// Ngày đăng ký khám
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// Thời gian đăng ký khám
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// Số lượng hẹn tối đa
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// Số hẹn
        /// </summary>
        public int bespeakNo { get; set; }

        /// <summary>
        /// Thời gian đến hẹn
        /// </summary>
        public DateTime? arrivalDate { get; set; }

        /// <summary>
        /// Nhân viên xử lý đăng ký khám tại chỗ
        /// </summary>
        public string arrivalOpereater { get; set; }

        /// <summary>
        /// Thời gian thao tác hẹn
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Nhân viên thao tác hẹn
        /// </summary>
        public string CreatorCode { get; set; }
    }
}
