using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class RegNewScheduleVO
    {
        /// <summary>
        /// Lịch trựcID
        /// </summary>
        public decimal ghpbId { get; set; }
        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// Khoa khám bệnh
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// Tên khoa khám bệnh
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Loại ngoại trú
        /// </summary>
        public string RegType { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Khoảng thời gian khám bệnh 1:Cả ngày 2:Buổi sáng 3:Buổi chiều
        /// </summary>
        public string Period { get; set; }
        /// <summary>
        /// Mô tả khám bệnh
        /// </summary>
        public string PeriodDesc { get; set; }
        /// <summary>
        /// Số đăng ký khám
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// Số đăng ký khám còn lại
        /// </summary>
        public int YYNum { get; set; }
        /// <summary>
        /// Thời gian bắt đầu khám bệnh
        /// </summary>
        public string PeriodStart { get; set; }
        /// <summary>
        /// Thời gian quyết toán khám bệnh
        /// </summary>
        public string PeriodEnd { get; set; }
        /// <summary>
        /// Hạng mục đăng ký khám
        /// </summary>
        public string ghlx { get; set; }
        /// <summary>
        /// Hạng mục điều trị
        /// </summary>
        public string zlxm { get; set; }
        /// <summary>
        /// Thứ mấy
        /// </summary>
        public string weekdd { get; set; }
        /// <summary>
        /// Tên hạng mục đăng ký khám
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// Tên hạng mục điều trị
        /// </summary>
        public string zlxmmc { get; set; }
        /// <summary>
        /// Mô tả lịch trực
        /// </summary>
        public string pbdesc { get; set; }
        /// <summary>
        /// Tương ứng với cũHISMã khoa
        /// </summary>
        public string ybksbm { get; set; }
        /// <summary>
        /// Ngày khám bệnh
        /// </summary>
        public DateTime? OutDate { get; set; }
        /// <summary>
        /// Chi phí
        /// </summary>
        public decimal? RegFee { get; set; }
        public string IsBook { get; set; }
    }

    public class MzghbookScheduleVO
    {
        /// <summary>
        /// Trạng thái hẹn khám 
        /// </summary>
        public string yyzt { get; set; }
        /// <summary>
        /// Lịch trựcID
        /// </summary>
        public decimal ScheduId { get; set; }
        /// <summary>
        /// Tên khoa khám bệnh
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Khoa khám bệnh
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// Hạng mục đăng ký khámCode
        /// </summary>
        public string ghlx { get; set; }
        /// <summary>
        /// Hạng mục điều trịCode
        /// </summary>
        public string zlxm { get; set; }

        public string sfxmmc { get; set; }
        public string zlxmmc { get; set; }
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int QueueNo { get; set; }

        public string ysgh { get; set; }
        public string ysxm { get; set; }
        /// <summary>
        /// Ngày khám bệnh
        /// </summary>
        public DateTime OutDate { get; set; }
        public string PeriodDesc { get; set; }
        /// <summary>
        /// Loại ngoại trú
        /// </summary>
        public string Regtype { get; set; }
        /// <summary>
        /// Tiêu đề lịch trực
        /// </summary>
        public string Title { get; set; }
        public string AppId { get; set; }
        public int BookId { get; set; }  

        public string xm { get; set; }
        public string xb { get; set; }
        public string kh { get; set; }
        public string zjh { get; set; }
        public string mzh { get; set; }
        public decimal RegFee { get; set; }
    }
}
