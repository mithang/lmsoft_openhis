using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class MzpbScheduleVO
    {
        public string ScheduId { get; set; }
        public string ghpbId { get; set; }
        /// <summary>
        /// Ngày khám
        /// </summary>
        public string OutDate { get; set; }
        public string ysgh { get; set; }
        public string ysxm { get; set; }
        public string czks { get; set; }
        public string czksmc { get; set; }
        /// <summary>
        /// mjzbz Loại ngoại trú
        /// </summary>
        public string RegType { get; set; }
        /// <summary>
        /// Mô tả lịch trực
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Khoảng thời gian 0không 1Cả ngày 2Buổi sáng 3Buổi chiều
        /// </summary>
        public string Period { get; set; }
        public string PeriodDesc { get; set; }
        /// <summary>
        /// Tổng số đăng ký khám
        /// </summary>
        public int? TotalNum { get; set; }
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
        public decimal? RegFee { get; set; }
        /// <summary>
        /// Có mở hẹn khám hay không
        /// </summary>
        public string IsBook { get; set; }
        /// <summary>
        /// Hạng mục đăng ký khám
        /// </summary>
        public string ghlx { get; set; }
        public string ghxmmc { get; set; }
        /// <summary>
        /// Hạng mục khám chữa bệnh
        /// </summary>
        public string zlxm { get; set; }
        public string zlxmmc { get; set; }
        /// <summary>
        /// Thứ
        /// </summary>
        public int? weekdd { get; set; }
        public string IsCancel { get; set; }
        public string CancelReason { get; set; }
        
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// Số đăng ký khám còn lại
        /// </summary>
        public int? BookNum { get; set; }
    }
}
