using System;
using System.Collections.Generic;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// đặt hẹnNgày
    /// </summary>
    public class SysBespeakRegisterDateTimeVO
    {
        /// <summary>
        /// Tạm thờiKhóa chính
        /// </summary>
        public string tmpId { get; set; }

        /// <summary>
        /// đặt hẹnNgày
        /// </summary>
        public string regDate { get; set; }

        /// <summary>
        /// 星kỳ
        /// </summary>
        public string week { get; set; }

        /// <summary>
        /// đặt hẹnThời gianđoạn
        /// </summary>
        public List<SysBespeakRegisterTimeVO> regTimes { get; set; }
    }

    /// <summary>
    /// đặt hẹnThời gian
    /// </summary>
    public class SysBespeakRegisterTimeVO
    {
        /// <summary>
        /// Tạm thờiKhóa chính
        /// </summary>
        public string tmpId { get; set; }

        /// <summary>
        /// xt_bespeakRegisterbảngID
        /// </summary>
        public string tabId { get; set; }

        /// <summary>
        /// đặt hẹnThời gian
        /// </summary>
        public DateTime regBeginTime { get; set; }

        /// <summary>
        /// đặt hẹnThời gian
        /// </summary>
        public DateTime regEndTime { get; set; }

        /// <summary>
        /// đặt hẹnThời gian  regBeginTimevàregEndTimekếthợp
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// nhấtlớnđặt hẹn ngườisố
        /// </summary>
        public int bespeakMaxCount { get; set; }
    }
}
