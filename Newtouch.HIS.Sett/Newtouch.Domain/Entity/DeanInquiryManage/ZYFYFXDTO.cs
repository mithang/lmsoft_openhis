using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    /// <summary>
    /// Phân tích chi phí nội trúDTO
    /// </summary>
    public class ZYFYFXDTO
    {
        /// <summary>
        /// Tổng chi phí nội trú
        /// </summary>
        public decimal? zfy { get; set; }
        /// <summary>
        /// Tổng chi phí nội trú-So với kỳ trước
        /// </summary>
        public string zfy_hb { get; set; }
        /// <summary>
        ///  Tổng chi phí nội trú-So với cùng kỳ
        /// </summary>
        public string zfy_tb { get; set; }
        /// <summary>
        /// Phí thuốc
        /// </summary>
        public decimal? ypf { get; set; }
        /// <summary>
        /// Phí vật tư tiêu hao
        /// </summary>
        public decimal? hcf { get; set; }
        /// <summary>
        /// Phí khám chữa bệnh
        /// </summary>
        public decimal? zlf { get; set; }
        /// <summary>
        /// Số người xuất viện
        /// </summary>
        public int? cyrs { get; set; }
        /// <summary>
        /// Số người xuất viện-So với cùng kỳ
        /// </summary>
        public string cyrs_hb { get; set; }
        /// <summary>
        /// Số người xuất viện-So với kỳ trước
        /// </summary>
        public string cyrs_tb { get; set; }
        /// <summary>
        /// Chi phí nội trú bình quân mỗi lần
        /// </summary>
        public decimal? jczyfy { get; set; }
        /// <summary>
        /// Phí khám chữa bệnh-So với kỳ trước
        /// </summary>
        public string jczyfy_hb { get; set; }
        /// <summary>
        /// Phí khám chữa bệnh-So với cùng kỳ
        /// </summary>
        public string jczyfy_tb { get; set; }
    }
    public class ZYFYFX_KSFYFXDTO
    {
        /// <summary>
        /// Khoa nội trú
        /// </summary>
        public string zyks { get; set; }
        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksdm { get; set; }
        /// <summary>
        /// Tổng chi phí
        /// </summary>
        public decimal? zfy { get; set; }
        /// <summary>
        /// Phí thuốc
        /// </summary>
        public decimal? ypf { get; set; }
        /// <summary>
        /// Phí thuốc-Tỷ trọng
        /// </summary>
        public string ypf_zb { get; set; }
        /// <summary>
        /// Phí vật tư tiêu hao
        /// </summary>
        public decimal? hcf { get; set; }
        /// <summary>
        /// Phí vật tư tiêu hao-Tỷ trọng
        /// </summary>
        public string hcf_zb { get; set; }
        /// <summary>
        /// Phí xét nghiệm kiểm tra
        /// </summary>
        public decimal? jyjcf { get; set; }
        /// <decimal>
        /// Phí tài liệu
        /// </summary>
        public decimal? zlf { get; set; }
        /// <summary>
        /// Khác
        /// </summary>
        public decimal? qt { get; set; }
    }
    /// <summary>
    /// Phân tích chi phí bệnh nhân xuất viện
    /// </summary>
    public class ZYFYFX_CYHZFYFXDTO
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int xh { get; set; }
        /// <summary>
        /// Phân loại chi phí bình quân mỗi lần
        /// </summary>
        public string cjfyfl { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// Tỷ trọng
        /// </summary>
        public string zb { get; set; }
    }
    public class CYHZFYFTJTDTO
    {
        public string  nf{ get; set; }
        public decimal? 次均总费用 { get; set; }
        public decimal? 次均药费 { get; set; }
        public decimal? 次均耗材费 { get; set; }
        public decimal? 次均检验检查费 { get; set; }
        public decimal? 次均治疗费 { get; set; }
        public decimal? 次均其他费 { get; set; }
    }
}
