using NewtouchHIS.Domain.Entity.PatientCenter;

namespace NewtouchHIS.WebAPI.Manage.Models.OrderCenter
{
    /// <summary>
    /// Bệnh nhânHóa đơn
    /// </summary>
    public class PatBillRequest
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string? zyh { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string? OrderNo { get; set; }
        /// <summary>
        /// Số tiền đơn hàng
        /// </summary>
        public decimal? OrderAmt { get; set; }
        /// <summary>
        /// Loại đơn hàng EnumOrderType 1 Ngoại trú,2 Nội trú
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
