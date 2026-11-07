using System;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient.API
{
    /// <summary>
    /// Khoahoặcchuyên giađãđăng ký khámtổngsố Tra cứu Quay lạibáovăn
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
        /// đãđặt hẹnđăng ký khámtổngsố
        /// </summary>
        public int alreadyBespeakRegisterCount { get; set; }
    }
}
