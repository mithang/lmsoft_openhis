using System;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// Điều kiện tra cứu lãi/lỗ
    /// </summary>
    public class LossAndProditSearchDTO
    {
        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime startTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime endTime { get; set; }

        /// <summary>
        /// Nguyên nhân lãi/lỗ
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// Từ khóa
        /// </summary>
        public string inputCode { get; set; }

        /// <summary>
        /// Cờ lãi/lỗ  0:Thừa/thiếu  1:Lãi  -1:Tất cả
        /// </summary>
        public string sybz { get; set; }
    }
}
