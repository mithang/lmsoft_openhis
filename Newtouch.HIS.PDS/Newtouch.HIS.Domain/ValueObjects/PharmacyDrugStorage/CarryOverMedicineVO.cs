using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// tra cứu đã kết chuyển của thuốc
    /// </summary>
    public class CarryOverMedicineVO
    {
        /// <summary>
        /// thời gian kết chuyển
        /// </summary>
        public string Jzsj { get; set; }

        /// <summary>
        /// kỳ sổ sách
        /// </summary>
        public string zq { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// số lượng kết chuyển
        /// </summary>
        public int? jzsl { get; set; }

        /// <summary>
        /// số lượng kết chuyển kèm đơn vị
        /// </summary>
        public string jzslstr { get; set; }

        /// <summary>
        /// đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// tần suất
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// tên nhà máy dược/nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// giá bán buôn
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// giá bán lẻ
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// tiền bán buôn
        /// </summary>
        public decimal? pfze { get; set; }

        /// <summary>
        /// tiền bán lẻ
        /// </summary>
        public decimal? lsze { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
