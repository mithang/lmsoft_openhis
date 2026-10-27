using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Bản ghi thao tác y lệnh thuốc nội trú
    /// </summary>
    [Table("zy_returnDrugApplyBillDetail")]
    [SugarTable("zy_returnDrugApplyBillDetail")]
    public class ZyReturnDrugApplyBillDetailEntity : IEntity<ZyReturnDrugApplyBillDetailEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Đơn xin trả thuốcId  zy_returnDrugApplyBill.Id
        /// </summary>
        public string rabId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Số lượng trả thuốc（Đơn vị bộ phận）
        /// </summary>
        public int tysl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi（tysl*zhyz=Số lượng đơn vị nhỏ nhất）  Số chia lẻ nội trú mặc định
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// Trạng thái：0-Không hiệu lực ；1-Hiệu lực
        /// </summary>
        public string zt { get; set; }

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

        public long lyxh { get; set; }

        public string zytyapplyno { get; set; }
    }
}