using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class MzpbScheduleResponse
    {
        /// <summary>
        /// Khám bệnh ngày 
        /// </summary>
        public DateTime OutDate { get; set; }
        public int ScheduId { get; set; }

        public string Doctor { get; set; }
        public string DoctorName { get; set; }
        public string OutDept { get; set; }
        public string OutDeptName { get; set; }
        /// <summary>
        /// mjzbz Loại ngoại trú
        /// </summary>
        public string RegType { get; set; }
        /// <summary>
        /// lịch trực mô tả
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Thời gian đoạn 0 không có 1 cả ngày 2 buổi sáng 3 buổi chiều
        /// </summary>
        public string Period { get; set; }
        public string PeriodDesc { get; set; }
        
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
        /// <summary>
        /// Phí đ ăng ký khám
        /// </summary>
        public decimal? RegFee { get; set; }
        /// <summary>
        /// Phí đ ăng ký khám
        /// </summary>
        public decimal? GhFee { get; set; }
        /// <summary>
        /// Phí chẩn đoán và điều trị
        /// </summary>
        public decimal? ZLFee { get; set; }
        /// <summary>
        /// Đ ăng ký khám hạng mục
        /// </summary>
        public string Ghlx { get; set; }
        public string GhxmName { get; set; }
        /// <summary>
        /// chẩn đoán và điều trị hạng mục
        /// </summary>
        public string Zlxm { get; set; }
        public string ZlxmName { get; set; }
        /// <summary>
        /// Thứ 
        /// </summary>
        public int? Weekdd { get; set; }
        /// <summary>
        /// Tổng số nguồn số
        /// </summary>
        public int? TotalNum { get; set; }
        /// <summary>
        /// còn lại nguồn số 
        /// </summary>
        public int? BookNum { get; set; }
    }
}
