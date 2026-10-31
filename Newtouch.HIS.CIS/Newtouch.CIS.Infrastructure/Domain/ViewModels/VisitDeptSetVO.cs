using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// rakhámBác sĩthông tin
    /// </summary>
    public class VisitDeptSetVO
    {
        /// <summary>
        /// viêncôngHọ tên
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// viêncôngMã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// sởthuộcKhoaMã
        /// </summary>
        public string subordinateDeptCode { get; set; }

        /// <summary>
        /// sởthuộcTên khoa
        /// </summary>
        public string subordinateDeptName { get; set; }
    }

    /// <summary>
    /// rakhámKhoachi tiết
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
        /// chứccôngTên
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// Bác sĩMã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// rakhámKhoaMã(góiBao gồm thuộcKhoa)
        /// </summary>
        public string visitksCode { get; set; }

        /// <summary>
        /// rakhámTên khoa(góiBao gồm thuộcKhoa)
        /// </summary>
        public string visitDeptName { get; set; }

        /// <summary>
        /// rakhámLoại  1-thông thườngNgoại trú;2-gấpkhám;3-chuyên giaNgoại trú
        /// </summary>
        public int czlx { get; set; }

        /// <summary>
        /// sởthuộcKhoaMã
        /// </summary>
        public string SubordinateDepartments { get; set; }

        /// <summary>
        /// sởthuộcTên khoa
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
