using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_DepartmentBinding")]
    public class SysDepartmentBindingEntity : IEntity<SysDepartmentBindingEntity>
    {
        /// <summary>
        /// Mã liên kết
        /// </summary>
        [Key]
        public string bddm { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        public string ks { get; set; }

        public string ksmc { get; set; }

        public string gh { get; set; }

        public string xm { get; set; }

        public bool xb { get; set; }

        public string zjlx { get; set; }

        public string zjh { get; set; }


        /// <summary>
        /// 0:Không hiệu lực; 1:Hiệu lực
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
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
