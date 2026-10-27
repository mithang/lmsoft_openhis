using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// Ngoại trúđặt hẹnTra cứuyêu cầubáovăn
    /// </summary>
    public class MzBespeakRegisterQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// chứngmụcLoại
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
