namespace NewtouchHIS.Lib.Base.Model.DRG
{
    public class DrgGroupResult
    {
        /// <summary>
        /// mang vào đối tượng nhậpMedicalRecordcủaIndextrường，khuyên dùng số bệnh án hoặc số nằm viện，giữ tính duy nhất
        /// </summary>
        public String Index;

        /// <summary>
        /// kết quả phân nhóm，“phân nhóm thành công”đại diện cho phân nhóm thành công，các định nghĩa khác xemDrgGroupStatuslớp
        /// </summary>
        public String status;
        /// <summary>
        /// quá trình phân nhóm，định dạng danh sách，bao gồmICDthông tin chuyển đổi mã hóa、thông tin tên，vàDRGbản ghi từng bước thao tác phân nhóm，là kết quả gì，đã áp dụng những quy tắc nào
        /// </summary>
        public List<String> messages;
        /// <summary>
        /// nhóm lớn chẩn đoán chính，phân nhóm thành công hoặc phân vào nhóm mơ hồ thì có giá trị
        /// </summary>
        public String mdc;
        /// <summary>
        /// lõiDRGmã nhóm，sau khi kiểm tra hợp lệ sẽ có giá trị，khi phân vào nhóm mơ hồ thì làQY，khi không phân được vào nhóm thì là00
        /// </summary>
        public String adrg;
        /// <summary>
        /// DRGmã nhóm，sau khi kiểm tra hợp lệ sẽ có giá trị，khi phân vào nhóm mơ hồ thì là*QY，khi không phân được vào nhóm thì là00
        /// </summary>
        public String drg;
        public DrgMedicalRecord record;
    }


    /// <summary>
    /// đầy đủdrgkết quả phân nhóm
    /// </summary>
    public class DrgMedicalRecordResult : DrgMedicalRecord
    {
        /// <summary>
        /// quá trình phân nhóm，định dạng danh sách，bao gồmICDthông tin chuyển đổi mã hóa、thông tin tên，vàDRGbản ghi từng bước thao tác phân nhóm，là kết quả gì，đã áp dụng những quy tắc nào
        /// </summary>
        public List<String> messages { get; set; }
        /// <summary>
        /// kết quả phân nhóm，“phân nhóm thành công”đại diện cho phân nhóm thành công，các định nghĩa khác xemDrgGroupStatuslớp
        /// </summary>
        public String status { get; set; }
        /// <summary>
        /// nhóm lớn chẩn đoán chính，phân nhóm thành công hoặc phân vào nhóm mơ hồ thì có giá trị
        /// </summary>
        public String mdc { get; set; }
        /// <summary>
        /// lõiDRGmã nhóm，sau khi kiểm tra hợp lệ sẽ có giá trị，khi phân vào nhóm mơ hồ thì làQY，khi không phân được vào nhóm thì là00
        /// </summary>
        public String adrg { get; set; }
        /// <summary>
        /// DRGmã nhóm，sau khi kiểm tra hợp lệ sẽ có giá trị，khi phân vào nhóm mơ hồ thì là*QY，khi không phân được vào nhóm thì là00
        /// </summary>
        public String drg { get; set; }

    }
    /// <summary>
    /// tải lên tệpdrgkết quả phân nhóm
    /// </summary>
    public class DrgGroupFileResult : DrgMedicalRecordResult
    {
        public string file { get; set; }
        public List<DrgMedicalRecordResult> rows { get; set; }
    }

}
