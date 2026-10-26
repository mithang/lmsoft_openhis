using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-11-06 10:22
    /// Mô tả：Hồ sơ nhân viên phẫu thuật
    /// </summary>
    [Table("OR_OpStaffRecord")]
    public class OROpStaffRecordEntity : IEntity<OROpStaffRecordEntity>
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
        /// Số đăng ký phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssxh { get; set; }

        /// <summary>
        /// Loại nhân viên 1 Bác sĩ phẫu thuật chính 2Bác sĩ phụ tá 3Y tá lưu động 4Y tá rửa tay 
        /// </summary>
        /// <returns></returns>
        public string rylb { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// <returns></returns>
        public string rygh { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        /// <returns></returns>
        public string ryxm { get; set; }

        /// <summary>
        /// Số thứ tự
        /// </summary>
        /// <returns></returns>
        public int px { get; set; }

        /// <summary>
        /// memo
        /// </summary>
        /// <returns></returns>
        public string memo { get; set; }

        /// <summary>
        /// Trạng thái     1:Hiệu lực  0：Không hiệu lực
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
        /// Thời gian sửa
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

    }
}