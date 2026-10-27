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
        /// liên kếtKhám bệnhbảng
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// billingHệ thốngcủaThu phíTrạng thái
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// Liệt kê 1 Thuốc TâyĐơn thuốc 2 Thuốc Đông yĐơn thuốc 3 Phục hồi chức năngĐơn thuốc 4 Xét nghiệmĐơn thuốc 5 Kiểm traĐơn thuốc 6 truyền dịchĐơn thuốc
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
        /// liên kếtbasekhocủa ngườiviênMã nhân viên
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
        /// Đồng bộCờ，làKhôngđãĐồng bộđếnHIS（saukiểm tralàKhôngđãĐồng bộ sửathànhtheo赖Thao tácghi chépbảng）
        /// </summary>
        public bool? tbbz { get; set; }

        /// <summary>
        /// 临giườngnguyên nhân
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Đăng kýGhi chú
        /// </summary>
        public string sqbz { get; set; }
        /// <summary>
        /// trảCờ
        /// </summary>
        public bool? tbz { get; set; }

        /// <summary>
        /// Số phiếu（xatrìnhy tếĐăng kýcầnguid）
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// Đơn thuốcnhãn JI tinhI JII tinhII MZ têsay
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

        public string isdzcf { get; set; }//điện tửĐơn thuốc 1 là  0 Không
    }
}
