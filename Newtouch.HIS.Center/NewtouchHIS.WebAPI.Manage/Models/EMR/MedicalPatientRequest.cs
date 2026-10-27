namespace NewtouchHIS.WebAPI.Manage.Models.EMR
{
    /// <summary>
    /// Loại danh sách bệnh nhân Đang nằm viện,Xuất viện,Ngoại trú
    /// </summary>
    public class MedicalPatientRequest
    {
        public string brbz{ get; set; }
        public DateTime ksrq { get; set; }
        public DateTime jsrq { get; set; }
        public string srz { get; set; }
    }
    /// <summary>
    /// Văn bản bệnh ántree và danh sách chẩn đoán
    /// </summary>
    public class MedicalHomeRequest
    { 
        /// <summary>
        /// Số nhập viện/Số ngoại trú
        /// </summary>
        public string zyh { get; set; }
    }
}
