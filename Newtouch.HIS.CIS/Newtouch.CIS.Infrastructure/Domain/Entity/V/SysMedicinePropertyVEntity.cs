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
        /// liên kếtThuốcId
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// ThuốcCode
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 0 thông thườngMục 1 đặcđặcMụcThuốc đặc biệtMô tảlàđặcđịnhcủabệnh nhânTính chấtmớicó thểSử dụngcủa.
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
        /// Điều trịPhương pháp(cùngdạng liên kết Cách dùng thuốc)
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
        /// dùnglượngĐơn vị
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
        /// Sử dụngSố ngày(Bảo hiểm y tếkiểm soátlượngCài đặtchữđoạn)
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Một lầnnhấtlớnLiều lượng(Bảo hiểm y tếkiểm soátlượngCài đặtchữđoạn)
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Một lầnnhấtlớnSố lượng(Bảo hiểm y tếkiểm soátlượngCài đặtchữđoạn)
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// lũytínhnhấtlớnLiều lượng(Bảo hiểm y tếkiểm soátlượngCài đặtchữđoạn)
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// lũytínhnhấtlớnSố lượng(Bảo hiểm y tếkiểm soátlượngCài đặtchữđoạn)
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// Số đăng ký
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// ThuốcđặcđặcThuộc tính
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// Phân loại thuốcCode
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
        /// cung cấphàngĐơn vị
        /// </summary>
        public string ghdw { get; set; }

        /// <summary>
        /// ThuốcNơi sản xuất
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
