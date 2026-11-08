using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Ngoại trú đã đặt hẹn đăng ký khám
    /// </summary>
    [SugarTable("mz_yygh")]
    public class MzyyghEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// tổ chức kết cấu trúc
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// xt_bespeakRegister Khóa chính ID
        /// </summary>
        public string brId { get; set; }

        /// <summary>
        /// chứng mục Loại
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// đặt hẹn số
        /// </summary>
        public int bespeakNo { get; set; }

        /// <summary>
        /// Thời gian đến hẹn
        /// </summary>
        public DateTime? arrivalDate { get; set; }

        /// <summary>
        /// hiện trường chỗ lý đăng ký khám người viên
        /// </summary>
        public string arrivalOpereater { get; set; }

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
