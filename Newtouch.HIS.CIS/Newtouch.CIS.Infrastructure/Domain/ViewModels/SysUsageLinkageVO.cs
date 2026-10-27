using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Cách dùngliênđộnggắn
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
        /// Tên mục
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// MụcMã nhóm lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// MụcTên nhóm lớn
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tạongười
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửangười
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái     1:Hiệu lực  0：Vô hiệu
        /// </summary>
        public string zt { get; set; }
    }
}
