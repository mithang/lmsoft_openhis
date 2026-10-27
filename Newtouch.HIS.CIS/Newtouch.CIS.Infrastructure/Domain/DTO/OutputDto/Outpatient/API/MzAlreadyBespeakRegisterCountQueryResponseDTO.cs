using System;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient.API
{
    /// <summary>
    /// Khoahoặcchuyên giađãđăng ký khámtổngsốTra cứuQuay lạibáovăn
    /// </summary>
    public class MzAlreadyBespeakRegisterCountQueryResponseDTO
    {
        /// <summary>
        /// KhoaMã
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Bác sĩMã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// đặt hẹnNgày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// đặt hẹnThời gian
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// đãđặt hẹnđăng ký khámtổngsố
        /// </summary>
        public int alreadyBespeakRegisterCount { get; set; }
    }
}
