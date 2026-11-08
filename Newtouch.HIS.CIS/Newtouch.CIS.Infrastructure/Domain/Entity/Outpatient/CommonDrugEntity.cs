using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("mz_cyyp")]
    public class CommonDrugEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string cyypId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Liệt kê 1 Khoa thường dùng 2 Cá nhân thường dùng
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// liên kếtbasekhocủa Khoa bảng
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// liên kếtbasekhocủa người viên bảng
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// liên kếtbasekhocủa Hệ thống Thuốc bảng
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// Dư thừa chữ đoạn
        /// </summary>
        public string zdmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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

    }
}
