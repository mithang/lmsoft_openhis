using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_ypsx")]
    public class SysMedicinePropertyVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ypsxId { get; set; }

        /// <summary>
        /// Id thuốc liên kết
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// Code thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 0 Hạng mục thông thường 1 Hạng mục đặc biệt, thuốc đặc biệt chỉ được sử dụng cho tính chất bệnh nhân cụ thể.
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// Thuốc quý
        /// </summary>
        public string gzy { get; set; }

        /// <summary>
        /// Thuốc gây mê
        /// </summary>
        public string mzy { get; set; }

        /// <summary>
        /// Thuốc tâm thần
        /// </summary>
        public string yljsy { get; set; }

        /// <summary>
        /// Phương pháp điều trị (cũng liên kết với cách dùng thuốc)
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// Lịch trình thời gian
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// Liều dùng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Mã bảo hiểm y tế
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// Số ngày sử dụng (trường thiết lập kiểm soát số lượng bảo hiểm y tế)
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Liều lượng tối đa mỗi lần (trường thiết lập kiểm soát số lượng bảo hiểm y tế)
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Số lượng tối đa mỗi lần (trường thiết lập kiểm soát số lượng bảo hiểm y tế)
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// Liều lượng tối đa tích lũy (trường thiết lập kiểm soát số lượng bảo hiểm y tế)
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// Số lượng tối đa tích lũy (trường thiết lập kiểm soát số lượng bảo hiểm y tế)
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// Số văn bản phê duyệt
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Thuộc tính đặc biệt của thuốc
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// Code phân loại thuốc
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// Loại ghi sổ    0: Thu phí làm tròn theo bữa 1: Thu phí làm tròn theo lần
        /// </summary>
        public string jzlx { get; set; }

        /// <summary>
        /// Hạn sử dụng mặc định
        /// </summary>
        public int? mrbzq { get; set; }

        /// <summary>
        /// Đơn vị cung cấp
        /// </summary>
        public string ghdw { get; set; }

        /// <summary>
        /// Xuất xứ thuốc
        /// </summary>
        public int? ypcd { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

    }
}
