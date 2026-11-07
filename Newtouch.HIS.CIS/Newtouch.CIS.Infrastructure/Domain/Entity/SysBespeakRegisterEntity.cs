using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Hệ thốngđặt hẹnđăng ký khám Bảo trì
    /// </summary>
    [SugarTable("xt_bespeakRegister")]
    public class SysBespeakRegisterEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúc
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Dấu hiệu ngoại trú/nội trú 1:thông thường Ngoại trú  2:gấpkhám   3:chuyên gia Ngoại trú
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// đăng ký khám Ngày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// đăng ký khám Bắt đầukhiđoạn
        /// </summary>
        public DateTime regBeginTime { get; set; }

        /// <summary>
        /// đăng ký khám Kết thúckhiđoạn
        /// </summary>
        public DateTime regEndTime { get; set; }

        /// <summary>
        /// đăng ký khámkhiđoạn,đăng ký khám Bắt đầukhiđoạnvàđăng ký khám Kết thúckhiđoạnkếthợp
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Khoa Mã
        /// </summary>
        public string departmentCode { get; set; }

        /// <summary>
        /// Bác sĩ Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// nhấtlớnđặt hẹnsố
        /// </summary>
        public int bespeakMaxCount { get; set; }

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
        /// Trạng thái     1:Hiệu lực  0:Vô hiệu
        /// </summary>
        public string zt { get; set; }
    }
}
