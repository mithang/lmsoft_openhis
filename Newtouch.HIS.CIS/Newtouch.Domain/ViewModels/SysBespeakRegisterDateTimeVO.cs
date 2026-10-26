using System;
using System.Collections.Generic;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Ngày hẹn
    /// </summary>
    public class SysBespeakRegisterDateTimeVO
    {
        /// <summary>
        /// Khóa chính tạm thời
        /// </summary>
        public string tmpId { get; set; }

        /// <summary>
        /// Ngày hẹn
        /// </summary>
        public string regDate { get; set; }

        /// <summary>
        /// Thứ
        /// </summary>
        public string week { get; set; }

        /// <summary>
        /// Khoảng thời gian hẹn
        /// </summary>
        public List<SysBespeakRegisterTimeVO> regTimes { get; set; }
    }

    /// <summary>
    /// Thời gian hẹn
    /// </summary>
    public class SysBespeakRegisterTimeVO
    {
        /// <summary>
        /// Khóa chính tạm thời
        /// </summary>
        public string tmpId { get; set; }

        /// <summary>
        /// xt_bespeakRegisterbảngID
        /// </summary>
        public string tabId { get; set; }

        /// <summary>
        /// Thời gian hẹn
        /// </summary>
        public DateTime regBeginTime { get; set; }

        /// <summary>
        /// Thời gian hẹn
        /// </summary>
        public DateTime regEndTime { get; set; }

        /// <summary>
        /// Thời gian hẹn  regBeginTimevàregEndTimeKết hợp
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Số người hẹn tối đa
        /// </summary>
        public int bespeakMaxCount { get; set; }
    }
}
