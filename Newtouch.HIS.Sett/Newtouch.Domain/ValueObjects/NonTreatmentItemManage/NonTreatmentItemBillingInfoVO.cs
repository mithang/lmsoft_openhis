using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects.NonTreatmentItemManage
{
    [NotMapped]
    public class NonTreatmentItemBillingInfoVO: NonTreatmentItemBillingEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string sfxmName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string smksName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string smryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// Số lượng trả lại
        /// </summary>
        public int? tsl { get; set; }

        /// <summary>
        /// Số tiền trả lại
        /// </summary>
        public decimal? tje { get; set; }

        /// <summary>
        /// Thời gian bán
        /// </summary>
        public DateTime? SaleTime { get; set; }

        /// <summary>
        /// Thời gian hoàn phí
        /// </summary>
        public DateTime? RefundTime { get; set; }

        /// <summary>
        /// Nhân viên hoàn phí
        /// </summary>
        public string tfryName { get; set; }
    }
}
