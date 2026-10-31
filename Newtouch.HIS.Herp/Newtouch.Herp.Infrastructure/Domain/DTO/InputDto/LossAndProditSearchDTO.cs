using System;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// Điều kiện truy vấn chênh lệch
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
        /// Nguyên nhân chênh lệch
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// Từ khóa
        /// </summary>
        public string inputCode { get; set; }

        /// <summary>
        /// Cờ chênh lệch  0:Lỗ  1:Lãi  -1:Tất cả
        /// </summary>
        public string sybz { get; set; }
    }
}
