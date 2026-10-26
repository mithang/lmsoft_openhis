using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// phát thuốc ngoại trú thông tin
    /// </summary>
    public class DrugDeliveryEntity
    {
        /// <summary>
        /// họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// tuổi
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// ngày phát thuốc
        /// </summary>
        public DateTime fyrq { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// tên nhà máy dược
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// số lượng kèm đơn vị
        /// </summary>
        public string sl { get; set; }

        /// <summary>
        /// giá nhập đơn giá đơn vị bộ phận
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// tổng tiền giá nhập
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// giá bán lẻ đơn giá đơn vị bộ phận
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// tổng tiền bán lẻ
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// đơn vị bộ phận
        /// </summary>
        public string bmdw { get; set; }
    }
}
