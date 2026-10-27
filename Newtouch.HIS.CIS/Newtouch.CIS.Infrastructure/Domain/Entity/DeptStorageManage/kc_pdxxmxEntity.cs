using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.CIS.Compat;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Tồn khoKiểm kêthông tinchi tiết
    /// </summary>
    [SugarTable("dept_pdxxmx")]
    public class KcPdxxmxEntity
    {
        /// <summary>
        /// Khóa chính chi tiếtID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Kiểm kêchínhbảngID
        /// </summary>
        public long pdId { get; set; }

        /// <summary>
        /// vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        ///  Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// lýluậnSố lượng
        /// </summary>
        public int llsl { get; set; }

        /// <summary>
        /// thực tếSố lượng
        /// </summary>
        public int sjsl { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

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
