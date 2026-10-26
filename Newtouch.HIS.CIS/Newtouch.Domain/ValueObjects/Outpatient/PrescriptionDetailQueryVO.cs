using Newtouch.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.ValueObjects
{
    [NotMapped]
    public class PrescriptionDetailQueryVO: PrescriptionDetailEntity
    {
        /// <summary>
        /// của bảng từ điển thuốcjldw
        /// </summary>
        public string redundant_jldw { get; set; }
        /// <summary>
        /// Lưu lựa chọnjldw cùngmcjldw
        /// </summary>
        public string mcjldwwwwwww { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pcmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// Số lượng bán lẻ đơn vị ngoại trú(đã kê,Khi tính tiền nhất định dùng đơn vị ngoại trú)
        /// </summary>
        public decimal? cls { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string yfmc { get; set; }
        /// <summary>
        /// Số lượng đo đơn vị
        /// </summary>
        public int? dwjls { get; set; }
        /// <summary>
        /// Chiến lược định giá
        /// </summary>
        public int? jjcl { get; set; }
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Tên khoa thực hiện
        /// </summary>
        public string zxksmc { get; set; }

        public string jxCode { get; set; }

        public bool? tbz { get; set; }
        public string sfdlCode { get; set; }
        public string sfdlmc { get; set; }
        public decimal? jlzhxs { get; set; }
        public int? zxcs { get; set; }
        public int? tieshu { get; set; }

    }
}
