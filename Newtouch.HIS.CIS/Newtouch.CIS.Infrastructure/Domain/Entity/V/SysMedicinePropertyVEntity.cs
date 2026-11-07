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
        /// 0 thông thường Mục 1 đặcđặc Mục Thuốc đặc biệt Mô tảlàđặcđịnhcủabệnh nhân Tính chấtmớicó thể Sử dụngcủa.
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// đắttrọngthuốc
        /// </summary>
        public string gzy { get; set; }

        /// <summary>
        /// têsaythuốc
        /// </summary>
        public string mzy { get; set; }

        /// <summary>
        /// tinhthầnthuốc
        /// </summary>
        public string yljsy { get; set; }

        /// <summary>
        /// Điều trị Phương pháp(cùngdạng liên kết Cách dùng thuốc)
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// Thời giananxếp
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// dùnglượng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// dùnglượng Đơn vị
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
        /// Sử dụng Số ngày(Bảo hiểm y tếkiểm soátlượng Cài đặtchữđoạn)
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Một lầnnhấtlớn Liều lượng(Bảo hiểm y tếkiểm soátlượng Cài đặtchữđoạn)
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Một lầnnhấtlớn Số lượng(Bảo hiểm y tếkiểm soátlượng Cài đặtchữđoạn)
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// lũytínhnhấtlớn Liều lượng(Bảo hiểm y tếkiểm soátlượng Cài đặtchữđoạn)
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// lũytínhnhấtlớn Số lượng(Bảo hiểm y tếkiểm soátlượng Cài đặtchữđoạn)
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// Số đăng ký
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Thuốcđặcđặc Thuộc tính
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
        /// Mặc địnhbảochấtkỳ
        /// </summary>
        public int? mrbzq { get; set; }

        /// <summary>
        /// cung cấphàng Đơn vị
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
