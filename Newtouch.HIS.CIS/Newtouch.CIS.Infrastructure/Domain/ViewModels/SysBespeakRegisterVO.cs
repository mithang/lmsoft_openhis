using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// đặt hẹnđăng ký khámQuản lýnội dung
    /// </summary>
    public class SysBespeakRegisterVO
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúc
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Dấu hiệu ngoại trú/nội trú 1：thông thườngNgoại trú  2：gấpkhám   3：chuyên giaNgoại trú
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// đăng ký khámNgày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// đăng ký khámThời gian
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// chuyên giaTên
        /// </summary>
        public string zjmc { get; set; }

        /// <summary>
        /// KhoaMã
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// chuyên giaMã nhân viên
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// nhấtlớnđặt hẹnsố
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// Trạng thái     1:Hiệu lực  0：Vô hiệu
        /// </summary>
        public string zt { get; set; }

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
    }
}
