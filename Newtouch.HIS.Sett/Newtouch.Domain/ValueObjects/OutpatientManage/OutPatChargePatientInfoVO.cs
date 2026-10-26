namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// VO Quyết toán ngoại trú/Hoàn phí Thông tin xuất viện của bệnh nhân(Số quyết toán BHYT,Số BHXH,Lý do khám,Khoa,Bác sĩ,Chẩn đoán)
    /// </summary>
    public class OutPatChargePatientInfoVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jzyy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdicd10 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ysmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }
    }

    public class RefundZtDot
    {
        public int jsmxnm { get; set; }
        public string ztId { get; set; }
        public decimal dj { get; set; }
        public decimal sl { get; set; }
        public int cfnm { get; set; }
    }
}
