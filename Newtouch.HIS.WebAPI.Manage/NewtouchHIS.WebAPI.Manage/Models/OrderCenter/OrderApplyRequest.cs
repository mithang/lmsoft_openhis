namespace NewtouchHIS.WebAPI.Manage.Models.OrderCenter
{
    public class OrderApplyRequest
    {
        /// <summary>
        /// loại đơn hàng EnumOrderType 1 khám ngoại trú，2 nội trú
        /// </summary>
        public int? OrderType { get; set; }

        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// định danh lần khám này  khám ngoại trú：mzh；nội trú：zyh
        /// </summary>
        public string VisitNo { get; set; }
        /// <summary>
        /// thông tin bệnh nhân
        /// </summary>
        public int patId { get; set; }
        /// <summary>
        /// địa chỉId
        /// </summary>
        public string dzId { get; set; }
    }


    public class OrderInfoBaseVO
    {
        /// <summary>
        /// số đơn hàng
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// loại đơn hàng EnumOrderType
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        public string? CardNo { get; set; }
        /// <summary>
        /// định danh lần khám này  khám ngoại trú：mzh；nội trú：zyh
        /// </summary>
        public string? VisitNo { get; set; }
        /// <summary>
        /// trạng thái đơn hàng EnumOrderStatus
        /// </summary>
        public int? OrderStu { get; set; }
        /// <summary>
        /// số tiền đơn hàng
        /// </summary>
        public decimal? OrderAmt { get; set; }
        /// <summary>
        /// thời gian quá hạn khóa
        /// </summary>
        public DateTime? LockExpTime { get; set; }
    }
}
