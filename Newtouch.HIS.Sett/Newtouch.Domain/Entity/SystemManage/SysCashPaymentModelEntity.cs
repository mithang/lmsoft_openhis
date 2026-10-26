using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Phương thức thanh toán tiền mặt hệ thống
    /// </summary>
    [Table("xt_xjzffs")]
    public class SysCashPaymentModelEntity : IEntity<SysCashPaymentModelEntity>
    {
        /// <summary>
        /// Mã phương thức thanh toán tiền mặt
        /// </summary>
        [Key]
        public int xjzffsbh { get; set; }

        /// <summary>
        /// Phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// Tên phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffsmc { get; set; }

        /// <summary>
        /// Tính chất tài khoản
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

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

    }
}