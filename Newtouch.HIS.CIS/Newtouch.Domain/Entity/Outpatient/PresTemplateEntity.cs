using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_cfmb")]
    public class PresTemplateEntity : IEntity<PresTemplateEntity>
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
        /// Enum 1 Thường dùng của khoa 2 Thường dùng cá nhân
        /// </summary>
        public int mblx { get; set; }

        /// <summary>
        /// Liên kếtbaseBảng khoa của CSDL
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Enum 1 Đơn thuốc tây 2 Đơn thuốc đông y 3 Đơn thuốc phục hồi chức năng 4 Đơn xét nghiệm 5 Đơn kiểm tra
        /// </summary>
        public int cflx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mbmc { get; set; }

        /// <summary>
        /// Bác sĩ tạo
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
