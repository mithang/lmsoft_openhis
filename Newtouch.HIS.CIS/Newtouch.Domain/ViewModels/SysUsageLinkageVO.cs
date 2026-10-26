using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Liên kết liên động cách dùng
    /// </summary>
    public class SysUsageLinkageVO
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Mã cách dùng
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// Tên cách dùng
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// Mã hạng mục
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// Tên hạng mục
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// Mã nhóm lớn hạng mục
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Tên nhóm lớn hạng mục
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái     1:Có hiệu lực  0:Vô hiệu
        /// </summary>
        public string zt { get; set; }
    }
}
