using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_cf")]
    public class PrescriptionEntity
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
        /// liên kết Khám bệnhbảng
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// billing Hệ thốngcủa Thu phí Trạng thái
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// Liệt kê 1 Thuốc Tây Đơn thuốc 2 Thuốc Đông y Đơn thuốc 3 Phục hồi chức năng Đơn thuốc 4 Xét nghiệm Đơn thuốc 5 Kiểm tra Đơn thuốc 6 truyền dịch Đơn thuốc
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
        /// liên kếtbasekhocủa ngườiviên Mã nhân viên
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
        /// Cờ đồng bộ: đã đồng bộ đến HIS hay chưa (sau khi kiểm tra, nếu đã đồng bộ thì sửa theo bảng ghi chép thao tác)
        /// </summary>
        public bool? tbbz { get; set; }

        /// <summary>
        /// Nguyên nhân lâm sàng
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Đăng ký Ghi chú
        /// </summary>
        public string sqbz { get; set; }
        /// <summary>
        /// trả Cờ
        /// </summary>
        public bool? tbz { get; set; }

        /// <summary>
        /// Số phiếu(xatrìnhy tế Đăng kýcầnguid)
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// Đơn thuốcnhãn JI tinhI JII tinh II MZ têsay
        /// </summary>
        public string cftag { get; set; }
        /// <summary>
        /// sắc hộphươngkiểu
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// sắc hộtrảsố
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// Dặn dò
        /// </summary>
        public string cfzt { get; set; }

        public string isdzcf { get; set; }//điện tử Đơn thuốc 1 là  0 Không
    }
}
