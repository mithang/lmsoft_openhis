using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Hệ thống（Đăng nhập）Người dùng
    /// </summary>
    [SugarTable("Sys_User")]
    public class SysUserEntity : EntityBase
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chứcId（Cấp cao nhất，Cấu hình hệ thống）
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Tài khoản đăng nhập
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Loại ngôn ngữ：enTiếng Anh
        /// </summary>
        public string LanguageType { get; set; }

        /// <summary>
        /// Mã sắp xếp
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
        /// Người sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
