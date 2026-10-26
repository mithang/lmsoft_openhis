using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_cf")]
    public class PrescriptionEntity : IEntity<PrescriptionEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string cfId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Liên kết bảng khám bệnh
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// billingTrạng thái thu phí hệ thống
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// Enum 1 Đơn thuốc tây 2 Đơn thuốc đông y 3 Đơn thuốc phục hồi chức năng 4 Đơn xét nghiệm 5 Đơn kiểm tra 6 Đơn truyền dịch
        /// </summary>
        public int cflx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// Liên kếtbaseMã nhân viên của CSDL
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// Cờ đồng bộ,Đã đồng bộ tới hay chưaHIS(Sau đó kiểm tra đã đồng bộ hay chưa Đổi thành dựa vào bảng ghi nhận thao tác)
        /// </summary>
        public bool? tbbz { get; set; }

        /// <summary>
        /// Lý do lâm sàng
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Ghi chú yêu cầu
        /// </summary>
        public string sqbz { get; set; }
        /// <summary>
        /// Cờ trả lại
        /// </summary>
        public bool? tbz { get; set; }

        /// <summary>
        /// Số phiếu yêu cầu(Yêu cầu khám chữa bệnh từ xa cầnguid)
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// Nhãn đơn thuốc JI Hướng thầnI JII Hướng thầnII MZ Gây mê
        /// </summary>
        public string cftag { get; set; }
        /// <summary>
        /// Cách sắc hộ
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// Số thang sắc hộ
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// Dặn dò
        /// </summary>
        public string cfzt { get; set; }

        public string isdzcf { get; set; }//Đơn thuốc điện tử 1 Có  0 Không
    }
}
