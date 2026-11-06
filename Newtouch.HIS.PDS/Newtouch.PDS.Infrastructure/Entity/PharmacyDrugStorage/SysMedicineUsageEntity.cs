using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Cách dùng thuốc hệ thống
    /// </summary>
    [Table("xt_ypyf")]
    [SugarTable("xt_ypyf")]
    [Obsolete("please use the view")]
    public class SysMedicineUsageEntity : IEntity<SysMedicineUsageEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public int yfId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'zbz' trong bang xt_ypyf
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'zbz'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string zbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'lb' trong bang xt_ypyf
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'lb'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string lb { get; set; }

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

    }
}
