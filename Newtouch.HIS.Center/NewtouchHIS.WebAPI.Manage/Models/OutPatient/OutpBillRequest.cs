namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    public class OutpPresInfoRequest: OutpRegistQueryRequest
    {
        /// <summary>
        /// Đơn thuốc Id
        /// </summary>
        public string? PresId { get; set; }
        /// <summary>
        /// Đơn thuốc Id Tập hợp
        /// </summary>
        public string[] PresIds { get; set; }
    }

    public class OutpPresInfoHisApiRequest
    {
        /// <summary>
        /// Đơn thuốc Id
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
        /// Đơn thuốc Id
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
