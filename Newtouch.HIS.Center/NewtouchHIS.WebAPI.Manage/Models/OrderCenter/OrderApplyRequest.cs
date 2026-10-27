namespace NewtouchHIS.WebAPI.Manage.Models.OrderCenter
{
    public class OrderApplyRequest
    {
        /// <summary>
        /// Loại đơn hàng EnumOrderType 1 Ngoại trú,2 Nội trú
        /// </summary>
        public int? OrderType { get; set; }

        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// Mã định danh lần khám này  Ngoại trú:mzh;Nội trú:zyh
        /// </summary>
        public string VisitNo { get; set; }
        /// <summary>
        /// Thông tin bệnh nhân
        /// </summary>
        public int patId { get; set; }
        /// <summary>
        /// Địa chỉId
        /// </summary>
        public string dzId { get; set; }
    }


    public class OrderInfoBaseVO
    {
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// Loại đơn hàng EnumOrderType
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        public string? CardNo { get; set; }
        /// <summary>
        /// Mã định danh lần khám này  Ngoại trú:mzh;Nội trú:zyh
        /// </summary>
        public string? VisitNo { get; set; }
        /// <summary>
        /// Trạng thái đơn hàng EnumOrderStatus
        /// </summary>
        public int? OrderStu { get; set; }
        /// <summary>
        /// Số tiền đơn hàng
        /// </summary>
        public decimal? OrderAmt { get; set; }
        /// <summary>
        /// Thời gian khóa quá hạn
        /// </summary>
        public DateTime? LockExpTime { get; set; }
    }
}
