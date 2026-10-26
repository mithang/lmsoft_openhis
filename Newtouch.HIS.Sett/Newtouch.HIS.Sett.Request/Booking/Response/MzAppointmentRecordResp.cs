using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class MzAppointmentRecordListResp
    {
        public string Dept { get; set; }
        public string RegType { get; set; }
        public int QueueNo { get; set; }
        public string PatName { get; set; }
        public string Gender { get; set; }
        public int BookID { get; set; }
        public string BookStatus { get; set; }
        public DateTime OutDate { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal? RegFee { get; set; }
    }

    public class MzAppointmentRecordResp
    {
        /// <summary>
        /// số đ ặt lịch 
        /// </summary>
        /// <returns></returns>
        public int BookId { get; set; }

        /// <summary>
        /// đ ặt lịch trạng thái 1 Đ ã约 2 Đ ãĐ ăng ký khám 3 Đ ãhủy đ ặt lịch enumMzyyzt
        /// </summary>
        /// <returns></returns>
        public string BookStatus { get; set; }

        /// <summary>
        /// Lịch trực ngoại trú ngày程
        /// </summary>
        /// <returns></returns>
        public int ScheduId { get; set; }

        /// <summary>
        /// brxz Đ ăng ký khám giờ tự trả BHYT
        /// </summary>
        /// <returns></returns>
        public string Ghxz { get; set; }

        public string PatName { get; set; }
        public string Gnder { get; set; }
        public string Lxdh { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        /// <returns></returns>
        public string CardNo { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        /// <returns></returns>
        public string Dept { get; set; }

        /// <summary>
        /// Bác sĩ
        /// </summary>
        /// <returns></returns>
        public string Doctor { get; set; }

        /// <summary>
        /// Khám bệnh ngày 
        /// </summary>
        /// <returns></returns>
        public DateTime OutDate { get; set; }

        /// <summary>
        /// Loại đ ăng ký khám ngoại trú cấp cứu chuyên gia
        /// </summary>
        /// <returns></returns>
        public string RegType { get; set; }

        public string RegName { get; set; }
        /// <summary>
        /// Thời gian đoạn 0 không có 1全ngày 2 buổi sáng 3 buổi chiều
        /// </summary>
        /// <returns></returns>
        public int? Period { get; set; }

        /// <summary>
        /// PeriodStart
        /// </summary>
        /// <returns></returns>
        public string PeriodStart { get; set; }

        /// <summary>
        /// PeriodEnd
        /// </summary>
        /// <returns></returns>
        public string PeriodEnd { get; set; }

        /// <summary>
        /// QueueNo
        /// </summary>
        /// <returns></returns>
        public int QueueNo { get; set; }

        /// <summary>
        /// RegFee
        /// </summary>
        /// <returns></returns>
        public decimal? RegFee { get; set; }

        /// <summary>
        /// Đ ã付chi phí
        /// </summary>
        /// <returns></returns>
        public decimal? PayFee { get; set; }

        /// <summary>
        /// thanh toán số thứ tự 
        /// </summary>
        /// <returns></returns>
        public string PayLsh { get; set; }

        /// <summary>
        /// thanh toán thời gian
        /// </summary>
        /// <returns></returns>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// CancelReason
        /// </summary>
        /// <returns></returns>
        public string CancelReason { get; set; }

        /// <summary>
        /// CancelTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// mzh
        /// </summary>
        /// <returns></returns>
        public string Mzh { get; set; }

        /// <summary>
        /// ghrq
        /// </summary>
        /// <returns></returns>
        public DateTime? Ghrq { get; set; }

    }
}
