using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// Chi tiết trả thuốc
    /// </summary>
    public class RefundedDrugVo
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Đơn giá(Đơn vị bộ phận)
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Tên hãng thuốc
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// Số lượng (Số lượng đơn vị bộ phận) Mặc định vớiktslGiống nhau
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Số lượng có thể trả (Số lượng đơn vị bộ phận) Mặc định vớislGiống nhau
        /// </summary>
        public int ktsl { get; set; }

        /// <summary>
        /// Đơn vị bộ phận
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Số lượng+Đơn vị
        /// </summary>
        public string slstr { get; set; }

    }
}