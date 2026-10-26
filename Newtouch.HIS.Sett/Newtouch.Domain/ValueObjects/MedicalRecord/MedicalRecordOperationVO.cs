using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Tính phí hạng mục nội trú Hạng mục của Thông tin chi tiết
    /// </summary>
    public class MedicalRecordOperationVO
    {
        /// <summary>
        /// Phẫu thuậtID
        /// </summary>
        public string ssId { get; set; }
        /// <summary>
        /// Mã chẩn đoán
        /// </summary>
        public string ssdm { get; set; }
        /// <summary>
        /// Tên chẩn đoán
        /// </summary>
        public string ssmc { get; set; }
    }

}
