using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_yfbm_yp")]
    public class SysPharmacyDepartmentOpenMedicineEntity : IEntity<SysPharmacyDepartmentOpenMedicineEntity>
    {
        /// <summary>
        /// Id khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Số hiệu phòng dược
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Số hiệu nhóm lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Id tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Người dùng sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối cùng
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người dùng sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái 0 - Không hiệu lực  1 - Có hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

    }
}
