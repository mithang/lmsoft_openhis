using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// chi tiết trả thuốc nội trú
    /// </summary>
    [Table("zy_tymx")]
    public class ZyTymxEntity : IEntity<ZyTymxEntity>
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
        /// số phiếu trả thuốc
        /// </summary>
        public string returnDrugBillNo { get; set; }

        /// <summary>
        /// số hiệu thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// thực hiện y lệnh ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// y lệnh ID
        /// </summary>
        public string yzId { get; set; }

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
        /// ghi chú
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// trạng thái  0-không hiệu lực  1-còn hiệu lực
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

        public string zytyapplyno { get; set; }
    }
}