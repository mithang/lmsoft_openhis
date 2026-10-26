using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// bản ghi thao tác y lệnh thuốc nội trú
    /// </summary>
    [Table("zy_ypyzczjl")]
    public class ZyYpyzczjlEntity : IEntity<ZyYpyzczjlEntity>
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// tổ chức Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// thuốc y lệnh thông tin ID zy_ypyzxx bảng khóa chính ID
        /// </summary>
        public long ypyzxxId { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 1: phát thuốc 2: trả thuốc
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// số hiệu thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// số lượng đơn vị nhỏ nhất
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        public string zytyapplyno { get; set; }

        /// <summary>
        /// phát thuốc nội trú đề nghị ID
        /// </summary>
        public string zyfyapplyno { get; set; }

        /// <summary>
        /// mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// có tách lẻ không
        /// 1: có
        /// 2: không
        /// </summary>
        public int? sfcl { get; set; }
    }
}
