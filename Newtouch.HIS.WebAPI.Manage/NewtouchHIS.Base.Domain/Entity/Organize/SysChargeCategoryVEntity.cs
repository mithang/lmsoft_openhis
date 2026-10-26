
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///khoa hệ thống
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_xt_sfdl", "SysChargeCategoryVEntity")]
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
        /// báo cáo thống kê nhóm thu phí lớn
        /// </summary>
        public string reportdlcode { get; set; }

        /// <summary>
        /// loại nhóm lớn，enum tương ứng EnumSfdlDllb 1thuốc 2hạng mục điều trị 3hạng mục không phải điều trị
        /// </summary>
        public int? dllb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
