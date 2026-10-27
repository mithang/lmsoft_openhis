namespace NewtouchHIS.Lib.Base.Model.DRG
{
    public class DrgGroupResult
    {
        /// <summary>
        /// Đưa vào trường Index của đối tượng đầu vào MedicalRecord, nên sử dụng số bệnh án hoặc số nằm viện, giữ tính duy nhất
        /// </summary>
        public String Index;

        /// <summary>
        /// Kết quả nhóm,“Nhóm thành công”đại diện cho nhóm thành công, các định nghĩa khác xem lớp DrgGroupStatus
        /// </summary>
        public String status;
        /// <summary>
        /// Quá trình nhóm, định dạng danh sách, bao gồm thông tin chuyển đổi mã ICD, thông tin tên, và bản ghi từng bước thao tác nhóm DRG, kết quả là gì, đã áp dụng những quy tắc nào
        /// </summary>
        public List<String> messages;
        /// <summary>
        /// Nhóm chẩn đoán chính lớn, có giá trị khi nhóm thành công hoặc nhóm vào nhóm mơ hồ
        /// </summary>
        public String mdc;
        /// <summary>
        /// Mã nhóm DRG cốt lõi, có giá trị sau khi kiểm tra đạt, khi nhóm vào nhóm mơ hồ là QY, khi không nhóm vào nhóm nào là00
        /// </summary>
        public String adrg;
        /// <summary>
        /// DRGMã nhóm, có giá trị sau khi kiểm tra đạt, khi nhóm vào nhóm mơ hồ là*QY, khi không nhóm vào nhóm nào là00
        /// </summary>
        public String drg;
        public DrgMedicalRecord record;
    }


    /// <summary>
    /// Kết quả nhóm drg đầy đủ
    /// </summary>
    public class DrgMedicalRecordResult : DrgMedicalRecord
    {
        /// <summary>
        /// Quá trình nhóm, định dạng danh sách, bao gồm thông tin chuyển đổi mã ICD, thông tin tên, và bản ghi từng bước thao tác nhóm DRG, kết quả là gì, đã áp dụng những quy tắc nào
        /// </summary>
        public List<String> messages { get; set; }
        /// <summary>
        /// Kết quả nhóm,“Nhóm thành công”đại diện cho nhóm thành công, các định nghĩa khác xem lớp DrgGroupStatus
        /// </summary>
        public String status { get; set; }
        /// <summary>
        /// Nhóm chẩn đoán chính lớn, có giá trị khi nhóm thành công hoặc nhóm vào nhóm mơ hồ
        /// </summary>
        public String mdc { get; set; }
        /// <summary>
        /// Mã nhóm DRG cốt lõi, có giá trị sau khi kiểm tra đạt, khi nhóm vào nhóm mơ hồ là QY, khi không nhóm vào nhóm nào là00
        /// </summary>
        public String adrg { get; set; }
        /// <summary>
        /// DRGMã nhóm, có giá trị sau khi kiểm tra đạt, khi nhóm vào nhóm mơ hồ là*QY, khi không nhóm vào nhóm nào là00
        /// </summary>
        public String drg { get; set; }

    }
    /// <summary>
    /// Kết quả nhóm drg tải lên tệp
    /// </summary>
    public class DrgGroupFileResult : DrgMedicalRecordResult
    {
        public string file { get; set; }
        public List<DrgMedicalRecordResult> rows { get; set; }
    }

}
