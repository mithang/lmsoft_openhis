using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("V_S_xt_ypsx")]
    public class SysMedicinePropertyVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ypsxId { get; set; }

        /// <summary>
        /// liên kết Thuốc Id
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// Thuốc Code
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 0 thông thường Mục 1 đặc đặc Mục Thuốc đặc biệt Mô tả là đặc định của bệnh nhân Tính chất mới có thể Sử dụng của.
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// đắt trọng thuốc
        /// </summary>
        public string gzy { get; set; }

        /// <summary>
        /// tê say thuốc
        /// </summary>
        public string mzy { get; set; }

        /// <summary>
        /// tinh thần thuốc
        /// </summary>
        public string yljsy { get; set; }

        /// <summary>
        /// Điều trị Phương pháp(cùng dạng liên kết Cách dùng thuốc)
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// Thời gian sắp xếp
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// dùng lượng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// dùng lượng Đơn vị
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Mã BHYT
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// Sử dụng Số ngày(Bảo hiểm y tế kiểm soát lượng Cài đặt chữ đoạn)
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Một lần nhất lớn Liều lượng(Bảo hiểm y tế kiểm soát lượng Cài đặt chữ đoạn)
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Một lần nhất lớn Số lượng(Bảo hiểm y tế kiểm soát lượng Cài đặt chữ đoạn)
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// lũy tính nhất lớn Liều lượng(Bảo hiểm y tế kiểm soát lượng Cài đặt chữ đoạn)
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// lũy tính nhất lớn Số lượng(Bảo hiểm y tế kiểm soát lượng Cài đặt chữ đoạn)
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// Số đăng ký
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Thuốc đặc đặc Thuộc tính
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// Phân loại thuốc Code
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// Loại ghi sổ: 0: thu phí làm tròn theo bữa 1: thu phí làm tròn theo lần
        /// </summary>
        public string jzlx { get; set; }

        /// <summary>
        /// Mặc định bảo chất kỳ
        /// </summary>
        public int? mrbzq { get; set; }

        /// <summary>
        /// cung cấp hàng Đơn vị
        /// </summary>
        public string ghdw { get; set; }

        /// <summary>
        /// Thuốc Nơi sản xuất
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
