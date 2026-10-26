using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_card")]
    public class SysCardEntity : IEntity<SysCardEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string CardId { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// Loại thẻ
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Tên loại thẻ
        /// </summary>
        public string CardTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hzxm { get; set; }

        /// <summary>
        /// Số BHYT cá nhân
        /// </summary>
        public string grbh { get; set; }

        /// <summary>
        /// Loại tham gia bảo hiểm
        /// </summary>
        public string cblb { get; set; }
        /// <summary>
        /// Mã nơi tham gia bảo hiểm
        /// </summary>
        public string cbdbm { get; set; }
        /// <summary>
        /// Loại hình bảo hiểm
        /// </summary>
        public string xzlx { get; set; }
        public string brxz { get; set; }

        public string accountattr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Memo { get; set; }

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
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
