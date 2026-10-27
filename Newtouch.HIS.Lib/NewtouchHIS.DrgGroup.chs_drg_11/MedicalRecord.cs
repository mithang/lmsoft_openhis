namespace NewtouchHIS.DrgGroup.chs_drg_11
{
    public class MedicalRecord
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
        public override String ToString()
        {
            String zdList_str = this.zdList == null || this.zdList.Length == 0 ? "" : "\"" + string.Join(",", this.zdList) + "\"";
            String ssList_str = this.ssList == null || this.ssList.Length == 0 ? "" : "\"" + string.Join(",", this.ssList) + "\"";
            return "MedicalRecord [Index=" + this.Index + ", gender=" + this.gender + ", age=" + this.age +
                ", ageDay=" + this.ageDay + ", weight=" + this.weight + ", dept=" + this.dept +
                ", inHospitalTime=" + this.inHospitalTime + ", leavingType=" + this.leavingType +
                ", zdList=" + zdList_str + ", ssList=" + ssList_str + ", remark=" + this.remark + "]";
        }
        public String ToInputString()
        {
            String zdList_str = this.zdList == null || this.zdList.Length == 0 ? "" : "\"" + string.Join("|", this.zdList) + "\"";
            String ssList_str = this.ssList == null || this.ssList.Length == 0 ? "" : "\"" + string.Join("|", this.ssList) + "\"";
            return $"{this.Index},{this.gender},{this.age},{this.ageDay},{this.weight},{this.dept},{this.inHospitalTime},{this.leavingType},{zdList_str},{ssList_str},{this.remark??""}";
        }
    }
    
}
