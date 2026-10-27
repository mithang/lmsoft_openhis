using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết trả thuốc nội trú
    /// </summary>
    [Table("zy_tymx")]
    [SugarTable("zy_tymx")]
    public class ZyTymxEntity : IEntity<ZyTymxEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số đơn trả thuốc
        /// </summary>
        public string returnDrugBillNo { get; set; }

        /// <summary>
        /// Số hiệu thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Thực hiện y lệnhID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }

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
        /// Ghi chú
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Trạng thái  0-Không hiệu lực  1-Hiệu lực
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

        public string zytyapplyno { get; set; }
    }
}