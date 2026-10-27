using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// đãđặt hẹnNgoại trúđăng ký khámthông tinTra cứuyêu cầubáovăn
    /// </summary>
    public class MzAlreadyBespeakRegisterCountQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// KhoaMã
        /// </summary>
        public string ksCode { get; set; }
        
        /// <summary>
        /// Bác sĩMã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// đặt hẹnNgày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// đặt hẹnThời gian
        /// </summary>
        public string regTime { get; set; }

    }
}
