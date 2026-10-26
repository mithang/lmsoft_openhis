using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 13:45
    /// Mô tả: Bảng quan hệ khoa bệnh án và khoa HIS
    /// </summary>
    [Table("mr_rel_dept")]
    public class MrreldeptEntity : IEntity<MrreldeptEntity>
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// baksId
        /// </summary>
        /// <returns></returns>
        public string baksId { get; set; }

        /// <summary>
        /// hisdept
        /// </summary>
        /// <returns></returns>
        public string hisdept { get; set; }

        /// <summary>
        /// hisdeptname
        /// </summary>
        /// <returns></returns>
        public string hisdeptname { get; set; }

        /// <summary>
        /// Trạng thái     1:Có hiệu lực, 0: Không hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

    }
}