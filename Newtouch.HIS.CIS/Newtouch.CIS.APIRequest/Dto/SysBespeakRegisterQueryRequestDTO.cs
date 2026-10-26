using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Bản tin yêu cầu xếp lịch đặt lịch hẹn khám ngoại trú
    /// </summary>
    public class SysBespeakRegisterQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// Mã khoa
        /// </summary>
        [Required]
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
        /// gKhung giờ đặt lịch
        /// </summary>
        public string regTime { get; set; }
    }
}
