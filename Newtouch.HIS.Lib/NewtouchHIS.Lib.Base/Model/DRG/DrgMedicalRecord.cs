namespace NewtouchHIS.Lib.Base.Model.DRG
{
    /// <summary>
    /// Thông tin nhóm Drg trang đầu bệnh án
    /// </summary>
    public class DrgMedicalRecord
    {
        /// <summary>
        /// Số trang đầu bệnh án
        /// Định danh duy nhất, không trùng lặp
        /// </summary>
        public String Index { get; set; }
        /// <summary>
        /// Mã giới tính
        /// </summary>
        public String gender { get; set; }
        /// <summary>
        /// Tuổi
        /// </summary>
        public int age { get; set; }
        /// <summary>
        /// Tuổi trẻ sơ sinh
        /// </summary>
        public int ageDay { get; set; }
        /// <summary>
        /// Cân nặng trẻ sơ sinh
        /// </summary>
        public int weight { get; set; }
        /// <summary>
        /// Khoa xuất viện
        /// </summary>
        public String dept { get; set; }
        /// <summary>
        /// Số ngày nằm viện
        /// </summary>
        public int inHospitalTime { get; set; }
        /// <summary>
        /// Cách thức rời viện
        /// </summary>
        public String leavingType { get; set; }
        /// <summary>
        /// Danh sách chẩn đoán
        /// </summary>
        public String[] zdList { get; set; }
        /// <summary>
        /// Danh sách phẫu thuật và thủ thuật
        /// </summary>
        public String[] ssList { get; set; }
        public String remark { get; set; }
        public String ToInputString()
        {
            String zdList_str = this.zdList == null || this.zdList.Length == 0 ? "" : "\"" + string.Join("|", this.zdList) + "\"";
            String ssList_str = this.ssList == null || this.ssList.Length == 0 ? "" : "\"" + string.Join("|", this.ssList) + "\"";
            return $"{this.Index},{this.gender},{this.age},{this.ageDay},{this.weight},{this.dept},{this.inHospitalTime},{this.leavingType},{zdList_str},{ssList_str},{this.remark ?? ""}";
        }
    }
    /// <summary>
    /// DRGThông tin bệnh án nhóm
    /// (with lựa chọn phiên bản)
    /// </summary>
    public class DrgAreaMedicalRecord : DrgMedicalRecord
    {
        /// <summary>
        /// Phiên bản bộ nhóm
        /// </summary>
        public string version { get; set; }
        public int? versionPolicy { get; set; }
    }
    /// <summary>
    /// DRGThông tin bệnh án nhóm
    /// (with lựa chọn phiên bản,List&Array)
    /// </summary>
    public class DrgAreaMedicalRecordS
    {
        /// <summary>
        /// Phiên bản bộ nhóm
        /// </summary>
        public string version { get; set; }
        public int? versionPolicy { get; set; }
        public List<DrgMedicalRecord>? medicalList { get; set; }
        public string[]? medicalArr { get; set; }
    }
}
