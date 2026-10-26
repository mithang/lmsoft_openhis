using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// nội trú _ trả thuốc phiếu đề nghị
    /// </summary>
    [Table("zy_returnDrugApplyBill")]
    public class ZyReturnDrugApplyBillEntity : IEntity<ZyReturnDrugApplyBillEntity>
    {
        /// <summary>
        /// khóa chính ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// phiếu đề nghị số
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// số nội trú
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// khoa mã
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// khu bệnh mã
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// xử lý trạng thái: 0-待处理; 1-xử lý trong; 2-xử lý hoàn thành 
        /// </summary>
        public int ProcessState { get; set; }

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

        public string zytyapplyno { get; set; }
    }
}