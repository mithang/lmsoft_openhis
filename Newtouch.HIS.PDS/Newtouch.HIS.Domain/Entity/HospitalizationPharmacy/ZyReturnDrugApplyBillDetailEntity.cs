using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// bản ghi thao tác y lệnh thuốc nội trú
    /// </summary>
    [Table("zy_returnDrugApplyBillDetail")]
    public class ZyReturnDrugApplyBillDetailEntity : IEntity<ZyReturnDrugApplyBillDetailEntity>
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// trả thuốc phiếu đề nghị Id  zy_returnDrugApplyBill.Id
        /// </summary>
        public string rabId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// số lượng trả thuốc (đơn vị bộ phận)
        /// </summary>
        public int tysl { get; set; }

        /// <summary>
        /// hệ số chuyển đổi (tysl*zhyz=số lượng đơn vị nhỏ nhất)  mặc định số tách lẻ nội trú
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// số nhóm
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// trạng thái: 0-không hiệu lực; 1-còn hiệu lực
        /// </summary>
        public string zt { get; set; }

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

        public long lyxh { get; set; }

        public string zytyapplyno { get; set; }
    }
}