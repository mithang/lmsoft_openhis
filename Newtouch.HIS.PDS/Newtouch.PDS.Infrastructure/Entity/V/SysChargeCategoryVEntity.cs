using Newtouch.Infrastructure;
using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_sfdl")]
    [SugarTable("V_S_xt_sfdl")]
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
        /// Báo cáo Thống kê Loại thu phí lớn
        /// </summary>
        public string reportdlcode { get; set; }

        /// <summary>
        /// Loại lớn,Enum tương ứng EnumSfdlDllb 1Thuốc 2Hạng mục điều trị 3Hạng mục không điều trị
        /// </summary>
        public int? dllb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
