using System;
using System.Collections.Generic;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// đặt hẹn Ngày
    /// </summary>
    public class SysBespeakRegisterDateTimeVO
    {
        /// <summary>
        /// Tạm thời Khóa chính
        /// </summary>
        public string tmpId { get; set; }

        /// <summary>
        /// đặt hẹn Ngày
        /// </summary>
        public string regDate { get; set; }

        /// <summary>
        /// Thứ trong tuần
        /// </summary>
        public string week { get; set; }

        /// <summary>
        /// đặt hẹn Thời gian đoạn
        /// </summary>
        public List<SysBespeakRegisterTimeVO> regTimes { get; set; }
    }

    /// <summary>
    /// đặt hẹn Thời gian
    /// </summary>
    public class SysBespeakRegisterTimeVO
    {
        /// <summary>
        /// Tạm thời Khóa chính
        /// </summary>
        public string tmpId { get; set; }

        /// <summary>
        /// xt_bespeakRegister bảng ID
        /// </summary>
        public string tabId { get; set; }

        /// <summary>
        /// đặt hẹn Thời gian
        /// </summary>
        public DateTime regBeginTime { get; set; }

        /// <summary>
        /// đặt hẹn Thời gian
        /// </summary>
        public DateTime regEndTime { get; set; }

        /// <summary>
        /// đặt hẹn Thời gian  regBeginTime và regEndTime kết hợp
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// nhất lớn đặt hẹn người số
        /// </summary>
        public int bespeakMaxCount { get; set; }
    }
}
