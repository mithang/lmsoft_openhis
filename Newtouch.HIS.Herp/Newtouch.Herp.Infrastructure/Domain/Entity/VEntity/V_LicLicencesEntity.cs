using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    public class VLicLicencesEntity
    {
        /// <summary>
        /// Khóa chính chứng chỉ
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Loại
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Thuộc về
        /// </summary>
        public string belonged { get; set; }

        /// <summary>
        /// Tên đối tượng
        /// </summary>
        public string objectName { get; set; }

        /// <summary>
        /// Số chứng chỉ
        /// </summary>
        public string licenceNo { get; set; }

        /// <summary>
        /// Ngày có hiệu lực
        /// </summary>
        public DateTime qxrq { get; set; }

        /// <summary>
        /// Ngày hết hiệu lực
        /// </summary>
        public DateTime sxrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fileUrl { get; set; }

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
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
