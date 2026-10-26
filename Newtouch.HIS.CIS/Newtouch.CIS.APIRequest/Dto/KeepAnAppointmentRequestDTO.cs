using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.CIS.APIRequest.Dto
{
    public class KeepAnAppointmentRequestDTO : RequestBase
    {

        /// <summary>
        /// Khóa chính đặt lịch hẹn khám mz_yygh.id
        /// </summary>
        public string mzyyghId { get; set; }

        /// <summary>
        /// Thời gian đến hẹn
        /// </summary>
        public DateTime arrivalDate { get; set; }

        /// <summary>
        /// Nhân viên xử lý đặt lịch tại chỗ
        /// </summary>
        public string arrivalOpereater { get; set; }
    }
}
