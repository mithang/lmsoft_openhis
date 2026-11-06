using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Phân loại thuốc hệ thống
    /// </summary>
    [Table("xt_ypfl")]
    [SugarTable("xt_ypfl")]
    [Obsolete("please use the view")]
    public class SysMedicineClassificationEntity : IEntity<SysMedicineClassificationEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public int ypflId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypflmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'OrganizeId' trong bang xt_ypfl
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'OrganizeId'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'TopOrganizeId' trong bang xt_ypfl
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'TopOrganizeId'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'flid' trong bang xt_ypfl
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'flid'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string flid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'flmc' trong bang xt_ypfl
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'flmc'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string flmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

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
        /// 1:Hiệu lực 0:Không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
