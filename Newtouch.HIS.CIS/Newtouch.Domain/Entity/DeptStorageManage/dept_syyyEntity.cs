using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Nguyên nhân chênh lệch tồn kho
    /// </summary>
    [Table("dept_syyy")]
    public class DeptKcSyyyEntity : IEntity<DeptKcSyyyEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Nguyên nhân thừa/thiếu
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// 0 Báo hao hụt,1 Báo thừa
        /// </summary>
        public string sybz { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
