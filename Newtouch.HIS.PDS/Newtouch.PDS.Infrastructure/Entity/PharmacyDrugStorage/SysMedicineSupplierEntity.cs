using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Nhà cung cấp thuốc hệ thống
    /// </summary>
    [Table("xt_ypgys")]
    [SugarTable("xt_ypgys")]
    [Obsolete("please use the view")]
    public class SysMedicineSupplierEntity : IEntity<SysMedicineSupplierEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public int gysId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gysCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        // NX21-003 (D-NX21-003): khong co cot 'TopOrganizeId' trong bang xt_ypgys
        // (kiem chung INFORMATION_SCHEMA tren NewtouchHIS_Base); SqlSugar map moi
        // property mac dinh -> SELECT/INSERT/UPDATE deu loi "Invalid column
        // name 'TopOrganizeId'". Khoi phuc hanh vi EF goc bang IsIgnore. Khong DDL (cam).
        [SugarColumn(IsIgnore = true)]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Mcsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Srm1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Srm2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Khyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Yhzh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Yzbm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lxr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lxdh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Bz { get; set; }

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

    }
}
