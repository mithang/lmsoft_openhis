using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Cài đặt khoa khám bệnh
    /// </summary>
    [Table("visit_deptSet")]
    public class VisitDeptSetEntity : IEntity<VisitDeptSetEntity>
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
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Mã khoa khám bệnh(Bao gồm khoa trực thuộc)
        /// </summary>
        public string visitksCode { get; set; }

        /// <summary>
        /// Loại khám bệnh  1-Khám thường ngoại trú;2-Cấp cứu;3-Khám chuyên gia
        /// </summary>
        public int czlx { get; set; }

        /// <summary>
        /// Mã khoa trực thuộc
        /// </summary>
        public string SubordinateDepartments { get; set; }

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