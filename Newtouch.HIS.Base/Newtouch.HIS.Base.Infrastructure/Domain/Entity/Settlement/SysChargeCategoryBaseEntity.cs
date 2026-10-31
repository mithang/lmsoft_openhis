using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_sfdl_base")]
    public class SysChargeCategoryBaseEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
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
        /// 0 Chung 1 Ngoại trú 2Nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// In báo cáo ngoại trú Thuộc về chi phí(so với phí ăn uống‘Khác’)
        /// </summary>
        public string mzprintreportcode { get; set; }

        /// <summary>
        /// Quyết toán ngoại trú Thuộc về chi phí(so với phí ăn uống‘Khác’)
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
        /// Loại nhóm lớn,Enum tương ứng EnumSfdlDllb 1Thuốc 2Hạng mục điều trị 3Hạng mục không điều trị
        /// </summary>
        public int? dllb { get; set; }

        public string fjCode { get; set; }
        public string fjmc { get; set; }

		public decimal? sn { get; set; }
	}
}
