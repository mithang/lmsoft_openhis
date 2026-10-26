using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    /// <summary>
    /// Bản tin trả về tra cứu tổng số đã đăng ký khám của khoa hoặc chuyên gia
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
        /// Ngày hẹn khám
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// Thời gian hẹn khám
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Tổng số đăng ký khám đã hẹn
        /// </summary>
        public int alreadyBespeakRegisterCount { get; set; }
    }
}
