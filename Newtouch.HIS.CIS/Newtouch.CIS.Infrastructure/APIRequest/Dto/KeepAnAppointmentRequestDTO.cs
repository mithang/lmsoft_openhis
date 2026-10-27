using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.CIS.APIRequest.Dto
{
    public class KeepAnAppointmentRequestDTO : RequestBase
    {

        /// <summary>
        /// đặt hẹnđăng ký khámKhóa chính mz_yygh.id
        /// </summary>
        public string mzyyghId { get; set; }

        /// <summary>
        /// 赴hẹnThời gian
        /// </summary>
        public DateTime arrivalDate { get; set; }

        /// <summary>
        /// hiệntrườngchỗlýđăng ký khám ngườiviên
        /// </summary>
        public string arrivalOpereater { get; set; }
    }
}
