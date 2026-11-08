using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.CIS.APIRequest.Dto
{
    public class KeepAnAppointmentRequestDTO : RequestBase
    {

        /// <summary>
        /// đặt hẹn đăng ký khám Khóa chính mz_yygh.id
        /// </summary>
        public string mzyyghId { get; set; }

        /// <summary>
        /// Thời gian đến hẹn
        /// </summary>
        public DateTime arrivalDate { get; set; }

        /// <summary>
        /// hiện trường chỗ lý đăng ký khám người viên
        /// </summary>
        public string arrivalOpereater { get; set; }
    }
}
