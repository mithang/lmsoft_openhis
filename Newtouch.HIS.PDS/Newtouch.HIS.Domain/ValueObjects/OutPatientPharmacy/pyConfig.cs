namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    public class pyConfig
    {
        /// <summary>
        /// 排药查询间隔时间
        /// </summary>
        public int pycxjgsj { get; set; }
        /// <summary>
        /// tự động xếp thuốc
        /// </summary>
        public string IsAutoPy { get; set; }
        /// <summary>
        /// tự động phát thuốc
        /// </summary>
        public string IsAutoFy { get; set; }
    }
}
