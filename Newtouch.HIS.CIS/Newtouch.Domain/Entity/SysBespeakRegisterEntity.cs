using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Bảo trì đăng ký khám theo hẹn hệ thống
    /// </summary>
    [Table("xt_bespeakRegister")]
    public class SysBespeakRegisterEntity : IEntity<SysBespeakRegisterEntity>
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
        /// Cờ ngoại trú/nội trú 1:Khám thường ngoại trú  2:Cấp cứu   3:Khám chuyên gia
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// Ngày đăng ký khám
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// Khung giờ bắt đầu đăng ký khám
        /// </summary>
        public DateTime regBeginTime { get; set; }

        /// <summary>
        /// Khung giờ kết thúc đăng ký khám
        /// </summary>
        public DateTime regEndTime { get; set; }

        /// <summary>
        /// Khung giờ đăng ký khám,Kết hợp khung giờ bắt đầu và kết thúc đăng ký khám
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string departmentCode { get; set; }

        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Số lượng hẹn tối đa
        /// </summary>
        public int bespeakMaxCount { get; set; }

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
