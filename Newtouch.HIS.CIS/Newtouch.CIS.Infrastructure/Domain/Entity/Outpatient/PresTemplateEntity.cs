using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_cfmb")]
    public class PresTemplateEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string mbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Liệt kê 1 Khoa thường dùng 2 Cá nhân thường dùng
        /// </summary>
        public int mblx { get; set; }

        /// <summary>
        /// liên kếtbasekhocủa Khoa bảng
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Liệt kê 1 Thuốc Tây Đơn thuốc 2 Thuốc Đông y Đơn thuốc 3 Phục hồi chức năng Đơn thuốc 4 Xét nghiệm Đơn thuốc 5 Kiểm tra Đơn thuốc
        /// </summary>
        public int cflx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mbmc { get; set; }

        /// <summary>
        /// tạo Bác sĩ
        /// </summary>
        public string ysgh { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }

    }
}
