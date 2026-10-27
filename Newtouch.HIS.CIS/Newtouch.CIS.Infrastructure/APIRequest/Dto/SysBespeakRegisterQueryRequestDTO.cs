using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Ngoại trúđăng ký khámXếp lịchyêu cầubáovăn
    /// </summary>
    public class SysBespeakRegisterQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// KhoaMã
        /// </summary>
        [Required]
        public string ksCode { get; set; }

        /// <summary>
        /// Bác sĩMã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// đăng ký khámNgày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// gđăng ký khámkhiđoạn
        /// </summary>
        public string regTime { get; set; }
    }
}
