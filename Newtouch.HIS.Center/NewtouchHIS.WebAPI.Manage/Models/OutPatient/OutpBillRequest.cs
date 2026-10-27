namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    public class OutpPresInfoRequest: OutpRegistQueryRequest
    {
        /// <summary>
        /// Đơn thuốcId
        /// </summary>
        public string? PresId { get; set; }
        /// <summary>
        /// Đơn thuốcId Tập hợp
        /// </summary>
        public string[] PresIds { get; set; }
    }

    public class OutpPresInfoHisApiRequest
    {
        /// <summary>
        /// Đơn thuốcId
        /// </summary>
        public string PresId { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string mzh { get; set; }
    }

    public class OutpBillRequest
    {
        /// <summary>
        /// Đơn thuốcId
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
        public string? OrderNo { get; set; }
        /// <summary>
        /// Số tiền đơn hàng
        /// </summary>
        public decimal? OrderAmt { get; set; }
    }
     

}
