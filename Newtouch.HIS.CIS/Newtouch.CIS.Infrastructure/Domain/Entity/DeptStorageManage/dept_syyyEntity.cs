using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Tồn khonguyên nhân lãi lỗ
    /// </summary>
    [SugarTable("dept_syyy")]
    public class DeptKcSyyyEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// hỏngthừanguyên nhân
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// 0 Báo hỏng,1 báo thừa
        /// </summary>
        public string sybz { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thứ tự
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
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa cuối người
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
