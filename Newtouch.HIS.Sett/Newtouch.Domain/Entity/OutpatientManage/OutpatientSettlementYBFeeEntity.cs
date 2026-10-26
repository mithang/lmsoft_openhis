using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi phí giao dịch BHYT quyết toán ngoại trú
    /// </summary>
    [Table("mz_js_ybjyfy")]
    public class OutpatientSettlementYBFeeEntity : IEntity<OutpatientSettlementYBFeeEntity>
    {
        /// <summary>
        /// Mã nội bộ chi phí
        /// </summary>
        [Key]
        public int ybfynm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// Số quyết toán BHYT(BHYT trả về)
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Tổng chi phí
        /// </summary>
        public decimal? ZFY { get; set; }
        /// <summary>
        /// Thanh toán tiền mặt
        /// </summary>
        public decimal? XJZF { get; set; }
        /// <summary>
        /// Chi phí thông thường(Bảo hiểm cư dân:Chi phí BHYT)
        /// </summary>
        public decimal? YBFY { get; set; }
        /// <summary>
        /// Chi phí đặc biệt
        /// </summary>
        public decimal? TSFY { get; set; }
        /// <summary>
        /// Chi trả cơ bản(Bảo hiểm cư dân:Thanh toán từ tài khoản)
        /// </summary>
        public decimal? JBZF { get; set; }
        /// <summary>
        /// Thanh toán trợ cấp công
        /// </summary>
        public decimal? GBZF { get; set; }
        /// <summary>
        /// Lũy kế chi phí thông thường được thanh toán trong đoạn tự chi trả ngoại trú(Mức khởi điểm bảo hiểm cư dân)
        /// </summary>
        public decimal? SUMZFDYBFY { get; set; }
        /// <summary>
        /// Chi phí thông thường được thanh toán trong đoạn tự chi trả ngoại trú(Bảo hiểm cư dân:Đoạn tự chi trả ngoại trú lần này)
        /// </summary>
        public decimal? ZFDYBFY { get; set; }
        /// <summary>
        /// Số dư cơ bản
        /// </summary>
        public decimal? JBYE { get; set; }
        /// <summary>
        /// Số dư trợ cấp công
        /// </summary>
        public decimal? GBYE { get; set; }
        /// <summary>
        /// Có thể đến trung tâm hoàn trả chi phí thông thường
        /// </summary>
        public decimal? KBXYBFY { get; set; }
        /// <summary>
        /// Có thể đến trung tâm hoàn trả chi phí đặc biệt
        /// </summary>
        public decimal? KBXTSFY { get; set; }
        /// <summary>
        /// (Bảo hiểm cư dân)Chi trả từ quỹ chung
        /// </summary>
        public decimal? TCZF { get; set; }
        /// <summary>
        /// (Bảo hiểm cư dân)Cứu trợ(Ưu đãi)Thanh toán
        /// </summary>
        public decimal? JZZF { get; set; }
        /// <summary>
        /// (Bảo hiểm cư dân)Thanh toán bổ sung bệnh nặng
        /// </summary>
        public decimal? DKC023 { get; set; }

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

    }
}
