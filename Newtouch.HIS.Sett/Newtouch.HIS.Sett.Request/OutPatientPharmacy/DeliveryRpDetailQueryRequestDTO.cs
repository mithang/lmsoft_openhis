using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// phát thuốc chi tiết đ ơn thuốctra cứu
    /// </summary>
    public class DeliveryRpDetailQueryRequestDto : RequestBase
    {

        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Nhà thuốc nhận thuốc 
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// ID tổ chức
        /// </summary>
        public string organizeId { get; set; }
    }
}