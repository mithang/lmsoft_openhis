using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Thông tin bác sĩ khám bệnh
    /// </summary>
    public class VisitDeptSetVO
    {
        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Mã khoa trực thuộc
        /// </summary>
        public string subordinateDeptCode { get; set; }

        /// <summary>
        /// Tên khoa trực thuộc
        /// </summary>
        public string subordinateDeptName { get; set; }
    }

    /// <summary>
    /// Chi tiết khoa khám bệnh
    /// </summary>
    public class VisitDeptDetail
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Mã khoa khám bệnh(Bao gồm khoa trực thuộc)
        /// </summary>
        public string visitksCode { get; set; }

        /// <summary>
        /// Tên khoa khám bệnh(Bao gồm khoa trực thuộc)
        /// </summary>
        public string visitDeptName { get; set; }

        /// <summary>
        /// Loại khám bệnh  1-Khám thường ngoại trú;2-Cấp cứu;3-Khám chuyên gia
        /// </summary>
        public int czlx { get; set; }

        /// <summary>
        /// Mã khoa trực thuộc
        /// </summary>
        public string SubordinateDepartments { get; set; }

        /// <summary>
        /// Tên khoa trực thuộc
        /// </summary>
        public string subordinateDeptName { get; set; }

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