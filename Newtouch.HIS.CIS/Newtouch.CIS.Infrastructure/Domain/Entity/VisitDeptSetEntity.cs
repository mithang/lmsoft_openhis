using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// ra khám Khoa Cài đặt
    /// </summary>
    [SugarTable("visit_deptSet")]
    public class VisitDeptSetEntity
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
        /// Bác sĩ Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// ra khám Khoa Mã(gói Bao gồm thuộc Khoa)
        /// </summary>
        public string visitksCode { get; set; }

        /// <summary>
        /// ra khám Loại  1-thông thường Ngoại trú;2-gấp khám;3-chuyên gia Ngoại trú
        /// </summary>
        public int czlx { get; set; }

        /// <summary>
        /// sở thuộc Khoa Mã
        /// </summary>
        public string SubordinateDepartments { get; set; }

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
