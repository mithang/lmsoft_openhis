using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Bản ghi thao tác y lệnh thuốc nội trú
    /// </summary>
    [Table("zy_ypyzczjl")]
    [SugarTable("zy_ypyzczjl")]
    public class ZyYpyzczjlEntity : IEntity<ZyYpyzczjlEntity>
    {
        /// <summary>
        /// Khóa chính ID
        /// </summary>
        [Key]
        // G33-F7: Id là IDENTITY trong DB — thiếu IsIdentity khiến SqlSugar INSERT kèm Id=0 → "Cannot insert explicit value for identity column"
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chức Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Thông tin y lệnh thuốc ID zy_ypyzxx Khóa chính bảng ID
        /// </summary>
        public long ypyzxxId { get; set; }

        /// <summary>
        /// Thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// Y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 1:Phát thuốc 2:Trả thuốc
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// Số hiệu thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Số lượng đơn vị nhỏ nhất
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        public string zytyapplyno { get; set; }

        /// <summary>
        /// Đơn xin phát thuốc nội trú ID
        /// </summary>
        public string zyfyapplyno { get; set; }

        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// 1: Có
        /// 2: Không
        /// </summary>
        public int? sfcl { get; set; }
    }
}
