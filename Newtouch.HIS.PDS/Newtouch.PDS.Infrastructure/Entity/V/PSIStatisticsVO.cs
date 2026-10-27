namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// Vùng chứa thống kê nhập xuất tồn
    /// </summary>
    public class PsiStatisticsVo
    {
        /// <summary>
        /// Đơn vị thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Số lượng đầu kỳ
        /// </summary>
        public int qcsl { get; set; }

        /// <summary>
        /// Số lượng nhập kho
        /// </summary>
        public int rksl { get; set; }

        /// <summary>
        /// Số lượng xuất kho
        /// </summary>
        public int cksl { get; set; }

        /// <summary>
        /// Số lượng lãi lỗ
        /// </summary>
        public int sysl { get; set; }

        /// <summary>
        /// Số lượng đầu kỳ
        /// </summary>
        public int qmsl { get; set; }

        /// <summary>
        /// Tổng giá bán buôn đầu kỳ
        /// </summary>
        public decimal qcpfze { get; set; }

        /// <summary>
        /// Tổng giá bán buôn nhập kho
        /// </summary>
        public decimal rkpfze { get; set; }

        /// <summary>
        /// Tổng giá bán buôn xuất kho
        /// </summary>
        public decimal ckpfze { get; set; }

        /// <summary>
        /// Tổng giá bán buôn lãi lỗ
        /// </summary>
        public decimal sypfze { get; set; }

        /// <summary>
        /// Tổng giá bán buôn cuối kỳ
        /// </summary>
        public decimal qmpfze { get; set; }

        /// <summary>
        /// Tổng lãi lỗ điều chỉnh giá
        /// </summary>
        public decimal tjsyze { get; set; }
        /// <summary>
        /// Số lượng thừa kiểm kê
        /// </summary>
        public int pysl { get; set; }
        /// <summary>
        /// Số lượng thiếu kiểm kê
        /// </summary>
        public int pksl { get; set; }
        /// <summary>
        /// Tổng thừa kiểm kê
        /// </summary>
        public decimal pyze { get; set; }
        /// <summary>
        /// Tổng thiếu kiểm kê
        /// </summary>
        public decimal pkze { get; set; }
    }
}
