using NewtouchHIS.Domain.Entity.PatientCenter;

namespace NewtouchHIS.WebAPI.Manage.Models.OrderCenter
{
    /// <summary>
    /// hóa đơn bệnh nhân
    /// </summary>
    public class PatBillRequest
    {
        /// <summary>
        /// số nội trú
        /// </summary>
        public string? zyh { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string? mzh { get; set; }
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// số giấy tờ
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// số đơn hàng
        /// </summary>
        public string? OrderNo { get; set; }
        /// <summary>
        /// số tiền đơn hàng
        /// </summary>
        public decimal? OrderAmt { get; set; }
        /// <summary>
        /// loại đơn hàng EnumOrderType 1 khám ngoại trú，2 nội trú
        /// </summary>
        public int? OrderType { get; set; }
        //public SysPatientAddressEntity AddressInfo { get; set; }
        public AddressInfo? addressInfo { get; set; }

        public class AddressInfo
        {
            public int patid { get; set; }
            public string? OrganizeId { get; set; }
            public string xm { get; set; }
            public string dh { get; set; }
            public string xian_sheng { get; set; }
            public string xian_shi { get; set; }
            public string xian_xian { get; set; }
            public string xian_dz { get; set; }
        }
    }
}
