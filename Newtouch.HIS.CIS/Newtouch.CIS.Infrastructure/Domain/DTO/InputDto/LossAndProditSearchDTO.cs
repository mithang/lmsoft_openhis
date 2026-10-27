using System;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// lãi lỗĐiều kiện tìm kiếm
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
        /// nguyên nhân lãi lỗ
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// Từ khóa
        /// </summary>
        public string inputCode { get; set; }

        /// <summary>
        /// lãi lỗCờ  0：hỏng  1：lợi  -1：Tất cả
        /// </summary>
        public string sybz { get; set; }
    }
}
