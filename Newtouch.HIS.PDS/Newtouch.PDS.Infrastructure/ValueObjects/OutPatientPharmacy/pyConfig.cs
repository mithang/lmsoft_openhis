namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    public class pyConfig
    {
        /// <summary>
        /// Thời gian khoảng cách truy vấn xếp thuốc
        /// </summary>
        public int pycxjgsj { get; set; }
        /// <summary>
        /// Tự động xếp thuốc
        /// </summary>
        public string IsAutoPy { get; set; }
        /// <summary>
        /// Tự động phát thuốc
        /// </summary>
        public string IsAutoFy { get; set; }
    }
}
