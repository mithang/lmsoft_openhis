using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// Hạng mục tính phí nội trúDto
    /// </summary>
    public class HosFeeItemInfoDto
    {
        /// <summary>
        /// Mã bảng tính phí hạng mục
        /// </summary>
        public int? xmjfbbh { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Ngày tạo(Ngày thực hiện)
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ssry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ssrymc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ssrq { get; set; }

        public decimal je { get; set; }
    }
}
