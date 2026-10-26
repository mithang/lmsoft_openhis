using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    public class OutpatientBenefitsEntiy
    {
        /// <summary>
        /// Khoa ngoại trú
        /// </summary>
        public string mzks { get; set; }
        /// <summary>
        /// Lượt khám ngoại trú
        /// </summary>
        public int? mzzlrc { get; set; }
        /// <summary>
        /// Thu nhập ngoại trú
        /// </summary>
        public decimal? mzsr { get; set; }
        /// <summary>
        /// Chi phí khám chữa bệnh bình quân đầu người
        /// </summary>
        public decimal? rjfy { get; set; }
        /// <summary>
        /// Tỷ trọng chi phí tự chi trả
        /// </summary>
        public decimal? zffyzb { get; set; }
        /// <summary>
        /// Tỷ trọng chi phí BHYT
        /// </summary>
        public decimal? ybfyzb { get; set; }
        /// <summary>
        /// Thời gian khám chữa bệnh trung bình
        /// </summary>
        public int? pjzlsc { get; set; }
        /// <summary>
        /// Tỷ lệ tái khám
        /// </summary>
        public decimal? fzl { get; set; }
        public string DeptmentCode { get; set; }
    }

    public class OutpatientBenefitsEntiy_Ysxy
    {
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string mzks { get; set; }
        /// <summary>
        /// Lượt khám ngoại trú
        /// </summary>
        public int? mzzlrc { get; set; }
        /// <summary>
        /// Thu nhập ngoại trú
        /// </summary>
        public decimal? mzsr { get; set; }
        /// <summary>
        /// Chi phí khám chữa bệnh bình quân đầu người
        /// </summary>
        public decimal? rjzlfy { get; set; }
        /// <summary>
        /// Tỷ trọng chi phí tự chi trả
        /// </summary>
        public decimal? zffyzb { get; set; }
        /// <summary>
        /// Tỷ trọng chi phí BHYT
        /// </summary>
        public decimal? ybfyzb { get; set; }
        /// <summary>
        /// Thời gian khám chữa bệnh trung bình
        /// </summary>
        public int? pjzlsc { get; set; }
        /// <summary>
        /// Tỷ lệ tái khám
        /// </summary>
        public decimal? fzl { get; set; }
    }
    /// <summary>
    /// Xếp hạng hiệu quả khoa
    /// </summary>
    public class KSXYPMDTO
    {
        public string Name { get; set; }
        public int? keyword { get; set; }
    }
    public class KSXYPMBySJSRDTO
    {
        public string Name { get; set; }
        public decimal? keyword { get; set; }
    }
    public class OutpatientBenefitsEntiy_Lsqs
    {
        /// <summary>
        /// Thứ Hai
        /// </summary>
        public int mon { get; set; }
        /// <summary>
        /// Thứ Ba
        /// </summary>
        public int tue { get; set; }
        /// <summary>
        /// Thứ Tư
        /// </summary>
        public int wed { get; set; }
        /// <summary>
        /// Thứ Năm
        /// </summary>
        public int thu { get; set; }
        /// <summary>
        /// Thứ Sáu
        /// </summary>
        public int fri { get; set; }
        /// <summary>
        /// Thứ Bảy
        /// </summary>
        public int sat { get; set; }
        /// <summary>
        /// Chủ Nhật
        /// </summary>
        public int sun { get; set; }
    }

}
