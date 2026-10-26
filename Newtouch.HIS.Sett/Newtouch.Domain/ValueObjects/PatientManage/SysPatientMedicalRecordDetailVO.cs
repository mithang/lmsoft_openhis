using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi tiết bệnh án
    /// </summary>
    public class SysPatientMedicalRecordDetailVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

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
        /// Thời gian tạo
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName { get; set; }

    }
}
