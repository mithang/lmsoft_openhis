using System;

namespace Newtouch.HIS.Domain.ValueObjects.DaySettleManage
{
    public class DailyFeeDataGirdDto
    { 
        /// <summary>
      /// Mã nội bộ quyết toán
      /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        public DateTime? jzsj { get; set; }
        /// <summary>
        /// Số ngoại trú của lần đăng ký khám tương ứng với quyết toán
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Tên tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; } 
        public decimal jszje { get; set; }

        public decimal ybbx { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public decimal? jsxjzf { get; set; }
        public string xb { get; set; }
        public string zjedx { get; set; }
        
    }
}
