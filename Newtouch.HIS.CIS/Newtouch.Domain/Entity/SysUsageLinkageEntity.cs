using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{

    /// <summary>
    /// Liên động cách dùng hệ thống
    /// </summary>
    [Table("xt_usageLinkage")]
    public class SysUsageLinkageEntity : IEntity<SysUsageLinkageEntity>
    {
        
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã cách dùng
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// Mã hạng mục
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// Mã nhóm lớn hạng mục
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái     1:Có hiệu lực  0:Vô hiệu
        /// </summary>
        public string zt { get; set; }
    }
}
