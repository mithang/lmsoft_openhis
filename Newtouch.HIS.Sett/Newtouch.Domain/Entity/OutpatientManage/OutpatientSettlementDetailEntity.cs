using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    ///Bảng quyết toán ngoại trú
    /// </summary>
    [Table("mz_jsmx")]
    public class OutpatientSettlementDetailEntity : IEntity<OutpatientSettlementDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int jsmxnm { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết đơn thuốc hạng mục điều trị
        /// </summary>
        public int? mxnm { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết đơn thuốc thuốc
        /// </summary>
        public int? cf_mxnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? sl { get; set; }

        /// <summary>
        /// 0 Đăng ký khám 1 Ghi sổ giường gia đình 2 Quyết toán ngoại trú(Bao gồm quyết toán giường gia đình) 3 Nhập viện 4 Thu chi tài khoản   -------   Khi hoàn phí,Phải biết có phải ghi sổ giường gia đình hay không,Xác định có thực hiện quyết toán BHYT hay không   －－－－   (gốc:0 Đăng ký khám 1 Đơn thuốc 2 Hạng mục  Vì đơn thuốc và hạng mục được quyết toán cùng lúc,Vì vậy không phân biệt)   
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// Số tiền giao dịch(dj*sl)
        /// </summary>
        public decimal? jyje { get; set; }

        /// <summary>
        /// Số tiền giao dịch BHYT
        /// </summary>
        public decimal? jyfwje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Số lượng có thể trả lại
        /// </summary>
        public decimal? ktsl { get; set; }

    }
}
