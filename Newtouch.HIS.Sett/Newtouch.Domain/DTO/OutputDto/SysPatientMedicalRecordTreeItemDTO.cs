namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Bệnh án lịch sử
    /// </summary>
    public class SysPatientMedicalRecordTreeItemDTO
    {
        /// <summary>
        /// Khóa chính duy nhất
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Ngày tháng
        /// </summary>
        public string rq { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Tên người tạo
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// Cấp chaId
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int attachType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string attachName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string attachPath { get; set; }

        /// <summary>
        /// Đường dẫn truy cập mạng
        /// </summary>
        public string attachUrl { get; set; }

        /// <summary>
        /// Thời gian tạo(Thời gian tải lên)
        /// </summary>
        public string DetailCreateTime { get; set; }

        /// <summary>
        /// Tên người tạo
        /// </summary>
        public string DetailCreatorUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
