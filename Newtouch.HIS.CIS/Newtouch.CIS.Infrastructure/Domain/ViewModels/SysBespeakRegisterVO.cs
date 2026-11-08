using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// đặt hẹn đăng ký khám Quản lý nội dung
    /// </summary>
    public class SysBespeakRegisterVO
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chức kết cấu trúc
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Dấu hiệu ngoại trú/nội trú 1:thông thường Ngoại trú  2:gấp khám   3:chuyên gia Ngoại trú
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// đăng ký khám Ngày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// đăng ký khám Thời gian
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// chuyên gia Tên
        /// </summary>
        public string zjmc { get; set; }

        /// <summary>
        /// Khoa Mã
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// chuyên gia Mã nhân viên
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// nhất lớn đặt hẹn số
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// Trạng thái     1:Hiệu lực  0:Vô hiệu
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tạo người
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửa người
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
