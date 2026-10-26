namespace NewtouchHIS.WebAPI.Manage.Models.EMR
{
    /// <summary>
    /// loại danh sách bệnh nhân đang nằm viện、xuất viện、khám ngoại trú
    /// </summary>
    public class MedicalPatientRequest
    {
        public string brbz{ get; set; }
        public DateTime ksrq { get; set; }
        public DateTime jsrq { get; set; }
        public string srz { get; set; }
    }
    /// <summary>
    /// văn bản bệnh ántree và danh sách chẩn đoán
    /// </summary>
    public class MedicalHomeRequest
    { 
        /// <summary>
        /// số nội trú/số khám ngoại trú
        /// </summary>
        public string zyh { get; set; }
    }
}
