using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class MzghBookVO
    {
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số hẹn khám
        /// </summary>
        /// <returns></returns>
        public decimal BookId { get; set; }

        /// <summary>
        /// Lịch trực ngoại trú
        /// </summary>
        /// <returns></returns>
        public decimal SchedId { get; set; }

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
        public int Period { get; set; }

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
        /// Số xếp hàng
        /// </summary>
        /// <returns></returns>
        public int? QueueNo { get; set; }

        /// <summary>
        /// Tổng chi phí đăng ký khám
        /// </summary>
        /// <returns></returns>
        public decimal RegFee { get; set; }

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
        /// Có hủy hay không
        /// </summary>
        /// <returns></returns>
        public string IsCancel { get; set; }

        /// <summary>
        /// Lý do hủy
        /// </summary>
        /// <returns></returns>
        public string CancelReason { get; set; }

        /// <summary>
        /// Thời gian hủy
        /// </summary>
        /// <returns></returns>
        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        /// <returns></returns>
        public string mzh { get; set; }

        /// <summary>
        /// Điện thoại liên hệ
        /// </summary>
        /// <returns></returns>
        public string lxdh { get; set; }

        /// <summary>
        /// Nguồn hẹn khám
        /// </summary>
        /// <returns></returns>
        public string AppName { get; set; }

        /// <summary>
        /// Nguồn hẹn khám
        /// </summary>
        /// <returns></returns>
        public string AppId { get; set; }
    }
}
