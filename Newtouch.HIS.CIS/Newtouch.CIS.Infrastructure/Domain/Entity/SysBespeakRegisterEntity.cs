using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Hệ thốngđặt hẹnđăng ký khámBảo trì
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
        /// Dấu hiệu ngoại trú/nội trú 1：thông thườngNgoại trú  2：gấpkhám   3：chuyên giaNgoại trú
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// đăng ký khámNgày
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// đăng ký khámBắt đầukhiđoạn
        /// </summary>
        public DateTime regBeginTime { get; set; }

        /// <summary>
        /// đăng ký khámKết thúckhiđoạn
        /// </summary>
        public DateTime regEndTime { get; set; }

        /// <summary>
        /// đăng ký khámkhiđoạn,đăng ký khámBắt đầukhiđoạnvàđăng ký khámKết thúckhiđoạnkếthợp
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// KhoaMã
        /// </summary>
        public string departmentCode { get; set; }

        /// <summary>
        /// Bác sĩMã nhân viên
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
        /// Trạng thái     1:Hiệu lực  0：Vô hiệu
        /// </summary>
        public string zt { get; set; }
    }
}
