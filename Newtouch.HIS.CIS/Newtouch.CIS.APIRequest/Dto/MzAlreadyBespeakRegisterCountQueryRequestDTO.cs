using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Bản tin yêu cầu tra cứu thông tin đặt lịch hẹn khám ngoại trú đã đặt
    /// </summary>
    public class MzAlreadyBespeakRegisterCountQueryRequestDTO : RequestBase
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
        /// Ngày đặt lịch
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// Thời gian đặt lịch
        /// </summary>
        public string regTime { get; set; }

    }
}
