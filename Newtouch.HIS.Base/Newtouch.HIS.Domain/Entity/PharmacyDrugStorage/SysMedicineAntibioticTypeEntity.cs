using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Danh mục kháng sinh
    /// </summary>
    [Table("xt_kssType")]
    public class SysMedicineAntibioticTypeEntity : IEntity<SysMedicineAntibioticTypeEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên danh mục
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Cấp danh mục
        /// </summary>
        public string typelevel { get; set; }

        /// <summary>
        /// Cấp độ quyền (0 Thuốc sử dụng không hạn chế, 1 Thuốc sử dụng hạn chế, 2 Thuốc sử dụng đặc biệt)
        /// </summary>
        public string qxjb { get; set; }

        /// <summary>
        /// Tên cấp độ quyền
        /// </summary>
        public string qxjbmc { get; set; }

        /// <summary>
        /// Id danh mục cấp trên
        /// </summary>
        public string parentId { get; set; }

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
        /// 1: Có hiệu lực 0: Không hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
