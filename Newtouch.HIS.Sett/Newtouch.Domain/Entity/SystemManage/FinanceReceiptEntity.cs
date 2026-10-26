using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Biên lai tài chính
    /// </summary>
    [Table("cw_sj")]
    public class FinanceReceiptEntity : IEntity<FinanceReceiptEntity>
    {
        /// <summary>
        /// Biên lai tài chínhID
        /// </summary>
        [Key]
        public string cwsjId { get; set; }

        /// <summary>
        /// Nhân viên
        /// </summary>
        public string ry { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Chữ cái đầu
        /// </summary>
        public string szm { get; set; }

        /// <summary>
        /// Số biên lai bắt đầu
        /// </summary>
        public long qssjh { get; set; }

        /// <summary>
        /// Số biên lai hiện tại
        /// </summary>
        public long dqsjh { get; set; }

        /// <summary>
        /// Số biên lai kết thúc
        /// </summary>
        public long? jssjh { get; set; }

        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
