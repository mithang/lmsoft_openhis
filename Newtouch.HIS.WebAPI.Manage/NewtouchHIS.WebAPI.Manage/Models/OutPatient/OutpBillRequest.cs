namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    public class OutpPresInfoRequest: OutpRegistQueryRequest
    {
        /// <summary>
        /// đơn thuốcId
        /// </summary>
        public string? PresId { get; set; }
        /// <summary>
        /// đơn thuốcId tập hợp
        /// </summary>
        public string[] PresIds { get; set; }
    }

    public class OutpPresInfoHisApiRequest
    {
        /// <summary>
        /// đơn thuốcId
        /// </summary>
        public string PresId { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }
    }

    public class OutpBillRequest
    {
        /// <summary>
        /// đơn thuốcId
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string? mzh { get; set; }
        public string? OrderNo { get; set; }
        /// <summary>
        /// số tiền đơn hàng
        /// </summary>
        public decimal? OrderAmt { get; set; }
    }
     

}
