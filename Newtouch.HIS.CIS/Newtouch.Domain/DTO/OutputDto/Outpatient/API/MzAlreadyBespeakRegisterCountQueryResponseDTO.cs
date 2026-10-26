using System;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient.API
{
    /// <summary>
    /// Thông báo trả về khi tra cứu tổng số đăng ký khám của khoa hoặc chuyên gia
    /// </summary>
    public class MzAlreadyBespeakRegisterCountQueryResponseDTO
    {
        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Ngày hẹn
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// Thời gian hẹn
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Tổng số đăng ký khám theo hẹn
        /// </summary>
        public int alreadyBespeakRegisterCount { get; set; }
    }
}
