using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    public class OutpatientComprehensiveAnalysisEntiy
    {
        /// <summary>
        /// Lượt khám ngoại trú
        /// </summary>
        public int mzrc { get; set; }
        /// <summary>
        /// Lượt người ngoại trú so với cùng kỳ
        /// </summary>
        public decimal? mzrc_tb { get; set; }
        /// <summary>
        /// Lượt người ngoại trú so với kỳ trước
        /// </summary>
        public decimal? mzrc_hb { get; set; }
        /// <summary>
        /// Số người ngoại trú
        /// </summary>
        public int? mzrs { get; set; }
        /// <summary>
        /// Số người ngoại trú so với cùng kỳ
        /// </summary>
        public decimal? mzrs_tb { get; set; }
        /// <summary>
        /// Số người ngoại trú so với kỳ trước
        /// </summary>
        public decimal? mzrs_hb { get; set; }
        /// <summary>
        /// Tỷ lệ tái khám
        /// </summary>
        public decimal? fzl { get; set; }
        /// <summary>
        /// Tỷ lệ tái khám so với cùng kỳ
        /// </summary>
        public decimal? fzl_tb { get; set; }
        /// <summary>
        /// Tỷ lệ tái khám so với kỳ trước
        /// </summary>
        public decimal? fzl_hb { get; set; }
        /// <summary>
        /// Chi phí mỗi lần khám
        /// </summary>
        public decimal? zcfy { get; set; }
        /// <summary>
        /// Chi phí mỗi lần khám so với cùng kỳ
        /// </summary>
        public decimal? zcfy_tb { get; set; }
        /// <summary>
        /// Chi phí mỗi lần khám so với kỳ trước
        /// </summary>
        public decimal? zcfy_hb { get; set; }
        /// <summary>
        /// Thời gian khám chữa bệnh trung bình
        /// </summary>
        public decimal? pjzlsc { get; set; }
        /// <summary>
        /// Thời gian khám chữa bệnh bình quân so với cùng kỳ
        /// </summary>
        public decimal? pjzlsc_tb { get; set; }
        /// <summary>
        /// Thời gian khám chữa bệnh bình quân so với kỳ trước
        /// </summary>
        public decimal? pjzlsc_hb { get; set; }
    }

    public class OutpatientComprehensiveAnalysisEntiy_Mzhzfx
    {
        /// <summary>
        /// Tỷ trọng tính chất bệnh nhân_BHYT
        /// </summary>
        public int hzxzb_yb { get; set; }
        /// <summary>
        /// Tỷ trọng tính chất bệnh nhân_Tự chi trả
        /// </summary>
        public int hzxzb_zf { get; set; }
        /// <summary>
        /// Tỷ trọng giới tính bệnh nhân ngoại trú_Nam giới
        /// </summary>
        public int mzhzxbzb_m { get; set; }
        /// <summary>
        /// Tỷ trọng giới tính bệnh nhân ngoại trú_Nữ giới
        /// </summary>
        public int mzhzxbzb_w { get; set; }
        /// <summary>
        ///  Các nhóm tuổi bệnh nhân khác nhau_3Dưới tuổi
        /// </summary>
        public int age_3 { get; set; }
        /// <summary>
        ///  Các nhóm tuổi bệnh nhân khác nhau_3-16tuổi
        /// </summary>
        public int age_3_16 { get; set; }
        /// <summary>
        ///  Các nhóm tuổi bệnh nhân khác nhau_17-65tuổi
        /// </summary>
        public int age_17_65 { get; set; }
        /// <summary>
        ///  Các nhóm tuổi bệnh nhân khác nhau_65-75tuổi
        /// </summary>
        public int age_65_75 { get; set; }
        /// <summary>
        ///  Các nhóm tuổi bệnh nhân khác nhau_75Trên tuổi
        /// </summary>
        public int age_75 { get; set; }
    }
}
