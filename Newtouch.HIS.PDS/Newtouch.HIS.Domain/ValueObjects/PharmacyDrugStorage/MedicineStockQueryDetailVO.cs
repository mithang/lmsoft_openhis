namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// tra cứu lượng tồn kho chi tiết
    /// </summary>
    public class MedicineStockQueryDetailVO
    {
        /// <summary>
        /// tên bộ phận
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public string yxq { get; set; }

        /// <summary>
        /// thuốc vị trí kho
        /// </summary>
        public string ypkw { get; set; }

        /// <summary>
        /// kiểm soát cờ
        /// </summary>
        public string kzbz { get; set; }

        /// <summary>
        /// số lượng đóng băng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// số lượng đóng băng
        /// </summary>
        //public int kcsl { get; set; }
        public string djslstr { get; set; }

        /// <summary>
        /// số lượng tồn kho
        /// </summary>
        //public int kcsl { get; set; }
        public string kcslstr { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// giá bán buôn bộ phận
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// giá bán lẻ bộ phận
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// tổng tiền giá bán buôn
        /// </summary>
        public decimal pjze { get; set; }

        /// <summary>
        /// giá bán lẻ tổng mức
        /// </summary>
        public decimal ljze { get; set; }

        /// <summary>
        /// giá nhập
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int? zhyz { get; set; }
    }
}
