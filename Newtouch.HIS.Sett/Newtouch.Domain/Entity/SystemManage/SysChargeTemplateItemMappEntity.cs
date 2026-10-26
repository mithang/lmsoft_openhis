using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_sfmbxm")]
    public class SysChargeTemplateItemMappEntity : IEntity<SysChargeTemplateItemMappEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string sfmbxmId { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfmbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        public int? sl { get; set; }
        public int? duration { get; set; }
        public string kflb { get; set; }

        /// <summary>
        /// Liều điều trị
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// Số lần thực hiện
        /// </summary>
        public int? zxcs { get; set; }

        /// <summary>
        /// Mã khoa thực hiện
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// Tần suất y lệnh
        /// </summary>
        public string yzpc { get; set; }

        /// <summary>
        /// Vị trí(Đề xuất điều trị phục hồi chức năng)
        /// </summary>
        public string bw { get; set; }

        public decimal? dj { get; set; }

    }
}
