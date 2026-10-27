using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thuốc của khoa dược
    /// </summary>
    public class PharmacyDepartmentOpenMedicineRepoVO
    {
        /// <summary>
        /// Khóa chínhId
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Số hiệu khoa dược
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tổ chức cấp cao nhấtId
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Số hiệu danh mục lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Sửa người dùng
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Tên khoa dược
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// Tên nhóm lớn
        /// </summary>
        public string dlmc { get; set; }

    }
}
