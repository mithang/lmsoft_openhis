using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// trả thuốc chi tiết
    /// </summary>
    public class RefundedDrugVo
    {
        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// đơn giá (đơn vị bộ phận)
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// tên nhà máy dược
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// số nhóm
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// số lượng  (số lượng đơn vị bộ phận) mặc định và ktsl giống nhau
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// số lượng có thể trả  (số lượng đơn vị bộ phận) mặc định và sl giống nhau
        /// </summary>
        public int ktsl { get; set; }

        /// <summary>
        /// đơn vị bộ phận
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// số lượng+đơn vị
        /// </summary>
        public string slstr { get; set; }

    }
}