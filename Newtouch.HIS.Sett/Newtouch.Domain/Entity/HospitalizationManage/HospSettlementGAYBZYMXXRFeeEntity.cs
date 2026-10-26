using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Tạo:lixin
    /// Ngày tháng:2019-05-26 14:00
    /// Mô tả:Quyết toán nội trú-Ghi chi tiết BHYT và trả về-Quý An
    /// </summary>
    [Table("zy_mxxr_gayb")]
    public class HospSettlementGAYBZYMXXRFeeEntity : IEntity<HospSettlementGAYBZYMXXRFeeEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string akc190 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string yka105 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string yka317 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string yka318 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string yka319 { get; set; }
        /// <summary>
        /// prm_yka110
        /// </summary>
        /// <returns></returns>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string message { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
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
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

    }
}