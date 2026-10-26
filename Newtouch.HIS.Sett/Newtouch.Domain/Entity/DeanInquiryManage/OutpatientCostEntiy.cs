using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    public class OutpatientCostEntiy
    {
        /// <summary>
        /// Phân loại thu nhập
        /// </summary>
        public string srfl { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal? je { get; set; }
    }

    /// <summary>
    /// Phân tích phân loại chi phí ngoại trú_Biểu đồ(Biểu đồ phân tích chi phí bình quân đầu người của bệnh nhân cũng dùng trường này, đều giống nhau)
    /// </summary>
    public class OutpatientCostEntiy_Mzfyflfx_tb
    {
        /// <summary>
        /// Năm
        /// </summary>
        public string 年份 { get; set; }
        /// <summary>
        /// Tổng chi phí
        /// </summary>
        public decimal? 总费用 { get; set; }
        /// <summary>
        /// Phí thuốc
        /// </summary>
        public decimal? 药品费 { get; set; }
        /// <summary>
        /// Phí vật tư tiêu hao
        /// </summary>
        public decimal? 耗材费 { get; set; }
        /// <summary>
        /// Phí xét nghiệm
        /// </summary>
        public decimal? 检验费 { get; set; }
        /// <summary>
        /// Phí kiểm tra
        /// </summary>
        public decimal? 检查费 { get; set; }
        /// <summary>
        /// Phí tài liệu
        /// </summary>
        public decimal? 资料费 { get; set; }
        /// <summary>
        /// Khác
        /// </summary>
        public decimal? 其他 { get; set; }
    }

    public class OutpatientCostEntiy_Hzrjfyfx
    {
        /// <summary>
        /// Phân loại chi phí bình quân đầu người
        /// </summary>
        public string srfl { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal? je { get; set; }
    }
}
