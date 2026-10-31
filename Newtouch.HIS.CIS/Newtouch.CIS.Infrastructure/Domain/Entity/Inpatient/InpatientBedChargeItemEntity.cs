using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tác giả: 冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:Nhập việnPhí giường bệnhdùnggắn
    /// </summary>
    [SugarTable("zy_cwbdfy")]
    public class InpatientBedChargeItemEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// BedCode
        /// </summary>
        /// <returns></returns>
        public string BedCode { get; set; }
        /// <summary>
        /// ChargeCode
        /// </summary>
        /// <returns></returns>
        public string ChargeCode { get; set; }
        /// <summary>
        /// ChargeName
        /// </summary>
        /// <returns></returns>
        public string ChargeName { get; set; }
        /// <summary>
        /// ChargeItem
        /// </summary>
        /// <returns></returns>
        public string ChargeItem { get; set; }
        /// <summary>
        /// ChargeUtity
        /// </summary>
        /// <returns></returns>
        public string ChargeUtity { get; set; }
        /// <summary>
        /// ChargeNum
        /// </summary>
        /// <returns></returns>
        public int ChargeNum { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 0Vô hiệu1Hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}
