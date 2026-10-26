using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Người liên hệ nhà cung cấp
    /// </summary>
    [Table("gys_contacts")]
    public class GysContactsEntity : IEntity<GysContactsEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên người liên hệ
        /// </summary>
        public string contactName { get; set; }

		/// <summary>
        /// Nhà cung cấp ID
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string telphone { get; set; }

        /// <summary>
        /// Trách nhiệm
        /// </summary>
        public string duties { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa cuối	
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
