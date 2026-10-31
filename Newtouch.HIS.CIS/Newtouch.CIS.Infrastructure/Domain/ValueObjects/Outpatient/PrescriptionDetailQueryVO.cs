using Newtouch.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.ValueObjects
{
    [NotMapped]
    public class PrescriptionDetailQueryVO: PrescriptionDetailEntity
    {
        /// <summary>
        /// Thuốcbảng từ điểncủajldw
        /// </summary>
        public string redundant_jldw { get; set; }
        /// <summary>
        /// Lưuchọncủajldw cùngmcjldw
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
        /// Đơn vị ngoại trútháokhôngsố(mởcủa,tínhtiềncủamộtđịnhlàdùngĐơn vị ngoại trú)
        /// </summary>
        public decimal? cls { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string yfmc { get; set; }
        /// <summary>
        /// Số lượng đơn vị tính
        /// </summary>
        public int? dwjls { get; set; }
        /// <summary>
        /// Chiến lược tính giá
        /// </summary>
        public int? jjcl { get; set; }
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Khoa thực hiệnTên
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
