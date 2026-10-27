using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Loại kháng sinh
    /// </summary>
    [SugarTable("xt_kssType")]
    public class SysMedicineAntibioticTypeEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Cấp loại
        /// </summary>
        public string typelevel { get; set; }

        /// <summary>
        /// Cấp quyền(0 Thuốc dùng không hạn chế,1 Thuốc dùng hạn chế,2 Thuốc sử dụng đặc biệt)
        /// </summary>
        public string qxjb { get; set; }

        /// <summary>
        /// Tên cấp quyền
        /// </summary>
        public string qxjbmc { get; set; }

        /// <summary>
        /// Loại cấp trênId
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
        /// 1：Hiệu lực 0：Vô hiệu
        /// </summary>
        public string zt { get; set; }
    }
}
