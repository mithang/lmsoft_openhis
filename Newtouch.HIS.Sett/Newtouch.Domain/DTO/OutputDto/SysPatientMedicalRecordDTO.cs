using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Bệnh án lịch sử
    /// </summary>
    public class SysPatientMedicalRecordDTO
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
        /// Thời gian tạo(Thời gian tải lên)
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// Chi tiết
        /// </summary>
        public IList<SysPatientMedicalRecordDetailVO> Details { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
