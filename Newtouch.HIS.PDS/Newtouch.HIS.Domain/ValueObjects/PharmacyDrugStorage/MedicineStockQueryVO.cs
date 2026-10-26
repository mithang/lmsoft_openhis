
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// tra cứu lượng tồn kho
    /// </summary>
    public class MedicineStockQueryVO
    {
        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// thuốc code
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }
        /// <summary>
        /// tên bộ phận
        /// </summary>
        public string deptName { get; set; }
        /// <summary>
        /// bộ phận nhà thuốc Code
        /// </summary>
        public string yfbmCode { get; set; }
        /// <summary>
        /// số thứ tự sổ sách
        /// </summary>
        public string zcxh { get; set; }
        /// <summary>
        /// tên nhà máy dược
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ bộ phận
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// giá bán buôn bộ phận
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// số lượng tồn kho
        /// </summary>
        //public int kcsl { get; set; }
        public string Kcslstr { get; set; }
        /// <summary>
        /// tổng tiền bán buôn
        /// </summary>
        public decimal pjze { get; set; }
        /// <summary>
        /// tổng tiền bán lẻ
        /// </summary>
        public decimal ljze { get; set; }
        /// <summary>
        /// phiên âm
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
