using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Số lô thực hiện y lệnh thuốc nội trú
    /// </summary>
    [Table("zy_ypyzzxph")]
    [SugarTable("zy_ypyzzxph")]
    public class ZyYpyzzxphEntity : IEntity<ZyYpyzzxphEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lượng đơn vị nhỏ nhất
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Mã nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// Trạng thái xếp kệ 0-Chưa xếp lên kệ  1-Đã xếp lên kệ
        /// </summary>
        public string gjzt { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// Thông tin thuốc nội trúID，zy_ypyzxxKhóa chính bảng
        /// </summary>
        public long? zyypxxId { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái  0-Không hiệu lực  1-Hiệu lực
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
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Nhân viên sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
