using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// ra khám Bác sĩ thông tin
    /// </summary>
    public class VisitDeptSetVO
    {
        /// <summary>
        /// viên công Họ tên
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// viên công Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// sở thuộc Khoa Mã
        /// </summary>
        public string subordinateDeptCode { get; set; }

        /// <summary>
        /// sở thuộc Tên khoa
        /// </summary>
        public string subordinateDeptName { get; set; }
    }

    /// <summary>
    /// ra khám Khoachi tiết
    /// </summary>
    public class VisitDeptDetail
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
        /// chức công Tên
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// Bác sĩ Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// ra khám Khoa Mã(gói Bao gồm thuộc Khoa)
        /// </summary>
        public string visitksCode { get; set; }

        /// <summary>
        /// ra khám Tên khoa(gói Bao gồm thuộc Khoa)
        /// </summary>
        public string visitDeptName { get; set; }

        /// <summary>
        /// ra khám Loại  1-thông thường Ngoại trú;2-gấp khám;3-chuyên gia Ngoại trú
        /// </summary>
        public int czlx { get; set; }

        /// <summary>
        /// sở thuộc Khoa Mã
        /// </summary>
        public string SubordinateDepartments { get; set; }

        /// <summary>
        /// sở thuộc Tên khoa
        /// </summary>
        public string subordinateDeptName { get; set; }

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
