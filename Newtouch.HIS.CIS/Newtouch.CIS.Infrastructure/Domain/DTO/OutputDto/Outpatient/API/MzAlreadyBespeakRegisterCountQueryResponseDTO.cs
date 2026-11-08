using System;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient.API
{
    /// <summary>
    /// Khoa hoặc chuyên gia đã đăng ký khám tổng số Tra cứu Quay lại báo văn
    /// </summary>
    public class MzAlreadyBespeakRegisterCountQueryResponseDTO
    {
        /// <summary>
        /// Khoa Mã
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Bác sĩ Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// đặt hẹn Ngày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// đặt hẹn Thời gian
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// đã đặt hẹn đăng ký khám tổng số
        /// </summary>
        public int alreadyBespeakRegisterCount { get; set; }
    }
}
