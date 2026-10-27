
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Truy vấn lượng tồn kho
    /// </summary>
    public class MedicineStockQueryVO
    {
        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Thuốccode
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }
        /// <summary>
        /// Tên bộ phận
        /// </summary>
        public string deptName { get; set; }
        /// <summary>
        /// Bộ phận nhà thuốcCode
        /// </summary>
        public string yfbmCode { get; set; }
        /// <summary>
        /// Số thứ tự sổ sách
        /// </summary>
        public string zcxh { get; set; }
        /// <summary>
        /// Tên hãng thuốc
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ bộ phận
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// Giá bán buôn bộ phận
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// Số lượng tồn kho
        /// </summary>
        //public int kcsl { get; set; }
        public string Kcslstr { get; set; }
        /// <summary>
        /// Tổng giá bán buôn
        /// </summary>
        public decimal pjze { get; set; }
        /// <summary>
        /// Tổng giá bán lẻ
        /// </summary>
        public decimal ljze { get; set; }
        /// <summary>
        /// Phiên âm
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
