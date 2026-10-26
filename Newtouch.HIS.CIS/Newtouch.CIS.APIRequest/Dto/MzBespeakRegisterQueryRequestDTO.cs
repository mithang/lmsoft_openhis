using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Bản tin yêu cầu tra cứu đặt lịch hẹn khám ngoại trú
    /// </summary>
    public class MzBespeakRegisterQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Số giấy tờ
        /// </summary>
        [Required]
        public string zjh { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        [Required]
        public string blh { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
    }
}
