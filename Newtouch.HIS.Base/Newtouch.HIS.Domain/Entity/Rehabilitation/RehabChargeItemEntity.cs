using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("kf_sfxm")]
    public class RehabChargeItemEntity : IEntity<RehabChargeItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string sfxmId { get; set; }

        /// <summary>
        /// Id cơ sở bệnh viện
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Phương thức tính phí, tương ứng với EnumCollectionMethod
        /// </summary>
        public int CollectionMethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfflCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
