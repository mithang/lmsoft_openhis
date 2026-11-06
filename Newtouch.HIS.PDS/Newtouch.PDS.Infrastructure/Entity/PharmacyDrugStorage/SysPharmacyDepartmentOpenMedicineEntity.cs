using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Hệ thống Bộ phận nhà thuốc Thuốc(Cấu hình nhà thuốc/Loại thu phí lớn kho thuốc mở)
    /// </summary>
    [Table("xt_yfbm_yp")]
    [SugarTable("xt_yfbm_yp")]
    [Obsolete("please use the view")]
    public class SysPharmacyDepartmentOpenMedicineEntity : IEntity<SysPharmacyDepartmentOpenMedicineEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'TopOrganizeId' trong bang xt_yfbm_yp
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'TopOrganizeId'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }

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
        // NX21-003 (D-NX21-003): cot zt trong bang xt_yfbm_yp la char,
        // khong phai int — entity cu khai int zt nen moi SELECT map deu
        // loi SqlSugar "zt can't convert string to int32" (harness NX21
        // chung minh ca khi bang 0 row). Doi sang string nhu moi entity
        // khac (zt='1'/'0'). Khong DDL (cam).
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
