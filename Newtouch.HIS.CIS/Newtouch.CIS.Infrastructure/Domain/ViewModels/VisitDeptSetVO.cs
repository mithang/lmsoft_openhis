using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// rakhám Bác sĩthông tin
    /// </summary>
    public class VisitDeptSetVO
    {
        /// <summary>
        /// viêncông Họ tên
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// viêncông Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// sởthuộc Khoa Mã
        /// </summary>
        public string subordinateDeptCode { get; set; }

        /// <summary>
        /// sởthuộc Tên khoa
        /// </summary>
        public string subordinateDeptName { get; set; }
    }

    /// <summary>
    /// rakhám Khoachi tiết
    /// </summary>
    public class VisitDeptDetail
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúc
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// chứccông Tên
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
        /// rakhám Khoa Mã(gói Bao gồm thuộc Khoa)
        /// </summary>
        public string visitksCode { get; set; }

        /// <summary>
        /// rakhám Tên khoa(gói Bao gồm thuộc Khoa)
        /// </summary>
        public string visitDeptName { get; set; }

        /// <summary>
        /// rakhám Loại  1-thông thường Ngoại trú;2-gấpkhám;3-chuyên gia Ngoại trú
        /// </summary>
        public int czlx { get; set; }

        /// <summary>
        /// sởthuộc Khoa Mã
        /// </summary>
        public string SubordinateDepartments { get; set; }

        /// <summary>
        /// sởthuộc Tên khoa
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
    }
}
