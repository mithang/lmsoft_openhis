namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// 进销存统计容器
    /// </summary>
    public class PsiStatisticsVo
    {
        /// <summary>
        /// thuốc đơn vị
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// giá nhập
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// đầu kỳ số lượng
        /// </summary>
        public int qcsl { get; set; }

        /// <summary>
        /// số lượng nhập kho
        /// </summary>
        public int rksl { get; set; }

        /// <summary>
        /// số lượng xuất kho
        /// </summary>
        public int cksl { get; set; }

        /// <summary>
        /// số lượng lãi/lỗ
        /// </summary>
        public int sysl { get; set; }

        /// <summary>
        /// đầu kỳ số lượng
        /// </summary>
        public int qmsl { get; set; }

        /// <summary>
        /// tổng tiền bán buôn đầu kỳ
        /// </summary>
        public decimal qcpfze { get; set; }

        /// <summary>
        /// nhập kho tổng tiền bán buôn
        /// </summary>
        public decimal rkpfze { get; set; }

        /// <summary>
        /// xuất kho tổng tiền bán buôn
        /// </summary>
        public decimal ckpfze { get; set; }

        /// <summary>
        /// lãi/lỗ tổng tiền bán buôn
        /// </summary>
        public decimal sypfze { get; set; }

        /// <summary>
        /// tổng tiền bán buôn cuối kỳ
        /// </summary>
        public decimal qmpfze { get; set; }

        /// <summary>
        /// tổng lãi/lỗ do điều chỉnh giá
        /// </summary>
        public decimal tjsyze { get; set; }
        /// <summary>
        /// số lượng thừa kiểm kê
        /// </summary>
        public int pysl { get; set; }
        /// <summary>
        /// số lượng thiếu kiểm kê
        /// </summary>
        public int pksl { get; set; }
        /// <summary>
        /// 盘盈总额
        /// </summary>
        public decimal pyze { get; set; }
        /// <summary>
        /// 盘亏总额
        /// </summary>
        public decimal pkze { get; set; }
    }
}
