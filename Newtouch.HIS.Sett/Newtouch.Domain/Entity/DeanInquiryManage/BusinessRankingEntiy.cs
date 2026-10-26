using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    /// <summary>
    /// Xếp hạng khối lượng công việc bác sĩ ngoại trú
    /// </summary>
    public class BusinessRankingEntiy
    {
        public string ys { get; set; }
        public string mzks { get; set; }
        public int mzl { get; set; }
    }

    /// <summary>
    /// Xếp hạng thu nhập khoa ngoại trú
    /// </summary>
    public class BusinessRankingEntiy_mzkssr
    {
        /// <summary>
        /// Khoa ngoại trú
        /// </summary>
        public string mzks { get; set; }
        /// <summary>
        /// Thu nhập ngoại trú
        /// </summary>
        public decimal mzsr { get; set; }
        /// <summary>
        /// Thu nhập thuốc
        /// </summary>
        public decimal ypsr { get; set; }
        /// <summary>
        /// Tỷ trọng thuốc
        /// </summary>
        public string ypzb { get; set; }
    }

    /// <summary>
    /// Xếp hạng khoa nghiệp vụ nội trú
    /// </summary>
    public class BusinessRankingEntiy_zyywks
    {

        public string bqCode { get; set; }
        /// <summary>
        /// Khoa nội trú
        /// </summary>
        public string zyks { get; set; }
        /// <summary>
        /// Số người xuất viện
        /// </summary>
        public int cyrs { get; set; }
        /// <summary>
        /// Số người nhập viện
        /// </summary>
        public int ryrs { get; set; }
        /// <summary>
        /// Số người phẫu thuật
        /// </summary>
        public int ssrs { get; set; }
        /// <summary>
        /// Tổng chi phí nội trú
        /// </summary>
        public decimal? zyzfy { get; set; }
        /// <summary>
        /// Chi phí bình quân mỗi lần
        /// </summary>
        public decimal? cjfy { get; set; }
        /// <summary>
        /// Số ngày nội trú trung bình
        /// </summary>
        public decimal? pjzyts { get; set; }
    }

    /// <summary>
    /// Xếp hạng nghiệp vụ bác sĩ
    /// </summary>
    public class BusinessRankingEntiy_ysyw
    {
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Số người xuất viện
        /// </summary>
        public int cyrs { get; set; }
        /// <summary>
        /// Số người nhập viện
        /// </summary>
        public int ryrs { get; set; }
        /// <summary>
        /// Số người phẫu thuật
        /// </summary>
        public int ssrs { get; set; }
        /// <summary>
        /// Tổng chi phí nội trú
        /// </summary>
        public decimal? zyzfy { get; set; }
        /// <summary>
        /// Chi phí bình quân mỗi lần
        /// </summary>
        public decimal? cjfy { get; set; }
        /// <summary>
        /// Số ngày nội trú trung bình
        /// </summary>
        public decimal? pjzyts { get; set; }
    }
}
