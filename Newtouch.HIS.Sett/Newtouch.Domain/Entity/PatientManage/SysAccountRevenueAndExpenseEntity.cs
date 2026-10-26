using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Bản ghi thu chi tài khoản hệ thống
    /// </summary>
    [Table("xt_zhszjl")]
    public class SysAccountRevenueAndExpenseEntity : IEntity<SysAccountRevenueAndExpenseEntity>
    {
        /// <summary>
        /// Số bản ghi thu chi tài khoản
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public int zhCode { get; set; }

        /// <summary>
        /// Mã nội bộ bệnh nhân
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// Số tiền thu chi
        /// </summary>
        public decimal szje { get; set; }

        /// <summary>
        /// Số dư tài khoản
        /// </summary>
        public decimal zhye { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        ///  Tính chất thu chi
        /// </summary>
        public int szxz { get; set; }

        /// <summary>
        /// Phương thức thanh toán tiền mặt Mã
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int? jsnm { get; set; }

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
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
