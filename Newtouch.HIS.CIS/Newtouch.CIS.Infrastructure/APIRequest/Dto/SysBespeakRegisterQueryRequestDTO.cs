using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Ngoại trú đăng ký khám Xếp lịch yêu cầu báo văn
    /// </summary>
    public class SysBespeakRegisterQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// Khoa Mã
        /// </summary>
        [Required]
        public string ksCode { get; set; }

        /// <summary>
        /// Bác sĩ Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// đăng ký khám Ngày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// gđăng ký khám khi đoạn
        /// </summary>
        public string regTime { get; set; }
    }
}
