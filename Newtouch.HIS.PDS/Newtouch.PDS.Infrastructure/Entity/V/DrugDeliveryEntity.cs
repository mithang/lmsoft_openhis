using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// Thông tin phát thuốc ngoại trú
    /// </summary>
    public class DrugDeliveryEntity
    {
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Ngày phát thuốc
        /// </summary>
        public DateTime fyrq { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Tên hãng thuốc
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Số lượng kèm đơn vị
        /// </summary>
        public string sl { get; set; }

        /// <summary>
        /// Giá nhập Đơn giá Đơn vị bộ phận
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Tổng giá nhập
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// Giá bán lẻ Đơn giá Đơn vị bộ phận
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// Đơn vị bộ phận
        /// </summary>
        public string bmdw { get; set; }
    }
}
