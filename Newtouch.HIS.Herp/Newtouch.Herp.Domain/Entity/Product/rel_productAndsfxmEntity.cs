using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity.Product
{
    /// <summary>
    /// Bảng đối chiếu khoản thu vật tư
    /// </summary>
    [Table("rel_productAndsfxm")]
    public class RelProductAndsfxmEntity : IEntity<RelProductAndsfxmEntity>
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
        /// Vật tư ID(Bắt buộc nhập)
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Mã nhóm thu lớn
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// Tên nhóm thu lớn
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// Mã khoản thu
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// Tên khoản thu
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// Trạng thái  0:Hủy bỏ;1.Hiệu lực
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
