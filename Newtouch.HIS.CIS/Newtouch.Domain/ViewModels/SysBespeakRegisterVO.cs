using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Nội dung quản lý đăng ký khám theo hẹn
    /// </summary>
    public class SysBespeakRegisterVO
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Cờ ngoại trú/nội trú 1:Khám thường ngoại trú  2:Cấp cứu   3:Khám chuyên gia
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// Ngày đăng ký khám
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// Thời gian đăng ký khám
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Tên chuyên gia
        /// </summary>
        public string zjmc { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Mã nhân viên chuyên gia
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// Số lượng hẹn tối đa
        /// </summary>
        public int bespeakMaxCount { get; set; }

        /// <summary>
        /// Trạng thái     1:Có hiệu lực  0:Vô hiệu
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
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
