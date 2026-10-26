using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// nội trú thuốc thực hiện y lệnh số lô
    /// </summary>
    [Table("zy_ypyzzxph")]
    public class ZyYpyzzxphEntity : IEntity<ZyYpyzzxphEntity>
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lượng đơn vị nhỏ nhất
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// nhà thuốc phát thuốc mã
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// xếp lên kệ trạng thái 0-chưa xếp lên kệ  1-đã xếp lên kệ
        /// </summary>
        public string gjzt { get; set; }

        /// <summary>
        /// số nhóm
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// nội trú thông tin thuốc ID, zy_ypyzxx bảng khóa chính
        /// </summary>
        public long? zyypxxId { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// trạng thái  0-không hiệu lực  1-còn hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuối viên
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
