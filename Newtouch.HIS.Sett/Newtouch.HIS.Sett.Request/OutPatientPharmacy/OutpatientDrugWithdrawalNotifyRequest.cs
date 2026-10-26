using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    public class OutpatientDrugWithdrawalNotifyRequest : RequestBase
    {
        /// <summary>
        /// Mã nội bộ đ ơn thuốc
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// Thuốc mã 
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// ID tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// trả thuốc số lư ợng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Số nhóm 
        /// </summary>
        public string czh { get; set; }

    }
}
