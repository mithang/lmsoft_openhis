using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Đăng ký khám theo hẹn ngoại trú
    /// </summary>
    [Table("mz_yygh")]
    public class MzyyghEntity : IEntity<MzyyghEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// xt_bespeakRegister Khóa chínhID
        /// </summary>
        public string brId { get; set; }

        /// <summary>
        /// Loại giấy tờ
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
        /// Số hẹn
        /// </summary>
        public int bespeakNo { get; set; }

        /// <summary>
        /// Thời gian đến hẹn
        /// </summary>
        public DateTime? arrivalDate { get; set; }

        /// <summary>
        /// Nhân viên xử lý đăng ký khám tại chỗ
        /// </summary>
        public string arrivalOpereater { get; set; }

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
