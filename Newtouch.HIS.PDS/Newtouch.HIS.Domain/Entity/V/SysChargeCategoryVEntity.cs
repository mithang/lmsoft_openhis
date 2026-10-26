using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_sfdl")]
    public class SysChargeCategoryVEntity
    {
        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzprintreportcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzprintbillcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// báo cáo thống kê thu phí nhóm lớn
        /// </summary>
        public string reportdlcode { get; set; }

        /// <summary>
        /// nhóm lớn loại, enum tương ứng EnumSfdlDllb 1 thuốc 2 điều trị hạng mục 3 hạng mục không điều trị
        /// </summary>
        public int? dllb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
