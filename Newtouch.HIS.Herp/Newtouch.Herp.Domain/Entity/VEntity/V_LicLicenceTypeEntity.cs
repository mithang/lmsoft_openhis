using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Loại giấy phép
    /// </summary>
    public class VLicLicenceTypeEntity
    {
        /// <summary>
        /// Loại ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Tên thuộc về
        /// </summary>
        public string belonged { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa cuối	
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
