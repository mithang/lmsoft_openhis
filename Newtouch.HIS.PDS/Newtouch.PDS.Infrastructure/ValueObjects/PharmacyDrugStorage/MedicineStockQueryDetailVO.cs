namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi tiết truy vấn lượng tồn kho
    /// </summary>
    public class MedicineStockQueryDetailVO
    {
        /// <summary>
        /// Tên bộ phận
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public string yxq { get; set; }

        /// <summary>
        /// Vị trí kho thuốc
        /// </summary>
        public string ypkw { get; set; }

        /// <summary>
        /// Cờ kiểm soát
        /// </summary>
        public string kzbz { get; set; }

        /// <summary>
        /// Số lượng đóng băng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// Số lượng đóng băng
        /// </summary>
        //public int kcsl { get; set; }
        public string djslstr { get; set; }

        /// <summary>
        /// Số lượng tồn kho
        /// </summary>
        //public int kcsl { get; set; }
        public string kcslstr { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// Giá bán buôn bộ phận
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// Giá bán lẻ bộ phận
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// Tổng giá bán buôn
        /// </summary>
        public decimal pjze { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ
        /// </summary>
        public decimal ljze { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int? zhyz { get; set; }
    }
}
