using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// đã đặt hẹn Ngoại trú đăng ký khám thông tinTra cứu yêu cầu báo văn
    /// </summary>
    public class MzAlreadyBespeakRegisterCountQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// Khoa Mã
        /// </summary>
        public string ksCode { get; set; }
        
        /// <summary>
        /// Bác sĩ Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// đặt hẹn Ngày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// đặt hẹn Thời gian
        /// </summary>
        public string regTime { get; set; }

    }
}
