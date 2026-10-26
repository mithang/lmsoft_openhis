using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class MzghBookRecordVO
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số hẹn khám
        /// </summary>
        /// <returns></returns>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal BookId { get; set; }

        /// <summary>
        /// Trạng thái hẹn khám 1 Đã hẹn 2 Đã đăng ký khám 3 Đã hủy hẹn khám EnumMzyyzt
        /// </summary>
        /// <returns></returns>
        public string yyzt { get; set; }

        /// <summary>
        /// Lịch trực ngoại trú
        /// </summary>
        /// <returns></returns>
        public decimal ScheduId { get; set; }

        /// <summary>
        /// brxz Khi đăng ký khám Tự chi trả BHYT
        /// </summary>
        /// <returns></returns>
        public string ghxz { get; set; }

        /// <summary>
        /// patid
        /// </summary>
        /// <returns></returns>
        public int? patid { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string lxdh { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        /// <returns></returns>
        public string kh { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        /// <returns></returns>
        public string ks { get; set; }

        /// <summary>
        /// Bác sĩ
        /// </summary>
        /// <returns></returns>
        public string ys { get; set; }

        /// <summary>
        /// Ngày khám
        /// </summary>
        /// <returns></returns>
        public DateTime OutDate { get; set; }

        /// <summary>
        /// Loại đăng ký khám Khám ngoại trú Cấp cứu Chuyên gia
        /// </summary>
        /// <returns></returns>
        public string RegType { get; set; }

        /// <summary>
        /// Khoảng thời gian 0không 1Cả ngày 2Buổi sáng 3Buổi chiều
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
        /// Chi phí đã trả
        /// </summary>
        /// <returns></returns>
        public decimal? PayFee { get; set; }

        /// <summary>
        /// Số giao dịch thanh toán
        /// </summary>
        /// <returns></returns>
        public string PayLsh { get; set; }

        /// <summary>
        /// Thời gian thanh toán
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
        public string mzh { get; set; }

        /// <summary>
        /// ghrq
        /// </summary>
        /// <returns></returns>
        public DateTime? ghrq { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        /// <returns></returns>
        public string AppId { get; set; }
        /// <summary>
        /// Nội dung hẹn khám
        /// </summary>
        public string yynr { get; set; }

    }
}
