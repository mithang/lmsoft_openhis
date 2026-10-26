using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_sfdl_base")]
    public class SysChargeCategoryBaseEntity : IEntity<SysChargeCategoryBaseEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int dlId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 0 Chung 1 Ngoại trú 2 Nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Báo cáo in ngoại trú, khoản chi phí thuộc về (ví dụ phí ăn uống được xếp vào ‘Khác’)
        /// </summary>
        public string mzprintreportcode { get; set; }

        /// <summary>
        /// Thanh toán ngoại trú, khoản chi phí thuộc về (ví dụ phí ăn uống được xếp vào ‘Khác’)
        /// </summary>
        public string mzprintbillcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Nhóm lớn báo cáo chung
        /// </summary>
        public string reportdlcode { get; set; }

        /// <summary>
        /// Danh mục nhóm lớn, tương ứng với enum EnumSfdlDllb 1 Thuốc 2 Hạng mục điều trị 3 Hạng mục không điều trị
        /// </summary>
        public int? dllb { get; set; }

        public string fjCode { get; set; }
        public string fjmc { get; set; }

		public decimal? sn { get; set; }
	}
}
