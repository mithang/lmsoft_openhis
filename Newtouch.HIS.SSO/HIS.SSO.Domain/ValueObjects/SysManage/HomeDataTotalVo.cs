namespace HIS.SSO.Domain.ValueObjects.SysManage
{
    public class HomeDataTotalVo : ystjVo
    {
        public string? brxz { get; set; }
        /// <summary>
        /// số lượng đăng ký khám
        /// </summary>
        public int? ghrc { get; set; }
        /// <summary>
        /// số lượng hủy đăng ký khám
        /// </summary>
        public int? thrc { get; set; }
        /// <summary>
        /// số tiền thanh toán
        /// </summary>
        public decimal? jsje { get; set; }
        /// <summary>
        /// số tiền hoàn trả
        /// </summary>
        public decimal? tfje { get; set; }

    }
    public class ystjVo : hstjVo
    {
        public int? cflx { get; set; }
        /// <summary>
        /// số lượng người khám
        /// </summary>
        public int? jzrc { get; set; }
        /// <summary>
        /// số tiền kê đơn
        /// </summary>
        public decimal? kfje { get; set; }
        /// <summary>
        /// số tiền thanh toán
        /// </summary>
        public decimal? jsje { get; set; }
    }
    public class hstjVo
    {
        /// <summary>
        /// số người đã vào khu
        /// </summary>
        public int? yrqrc { get; set; }
        /// <summary>
        /// số người chưa vào khu
        /// </summary>
        public int? wrqrc { get; set; }
        /// <summary>
        /// số lượng chưa ra khỏi khu trong ngày
        /// </summary>
        public int? jrwcqrc { get; set; }
        /// <summary>
        /// số lượng ra khỏi khu trong ngày
        /// </summary>
        public int? jrcqrc { get; set; }
        /// <summary>
        /// tổng số giường
        /// </summary>
        public int? zcw { get; set; }
        /// <summary>
        /// giường trống còn lại
        /// </summary>
        public int? sycw { get; set; }
    }
}
