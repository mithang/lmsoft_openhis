using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Truy vấn thuốc đã kết chuyển
    /// </summary>
    public class CarryOverMedicineVO
    {
        /// <summary>
        /// Thời gian kết chuyển
        /// </summary>
        public string Jzsj { get; set; }

        /// <summary>
        /// Kỳ sổ sách
        /// </summary>
        public string zq { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Số lượng kết chuyển
        /// </summary>
        public int? jzsl { get; set; }

        /// <summary>
        /// Số lượng kết chuyển kèm đơn vị
        /// </summary>
        public string jzslstr { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Tần suất
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Tên hãng thuốc/Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Giá bán buôn
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// Số tiền bán buôn
        /// </summary>
        public decimal? pfze { get; set; }

        /// <summary>
        /// Số tiền bán lẻ
        /// </summary>
        public decimal? lsze { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
