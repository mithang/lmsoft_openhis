using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thuốc của phòng dược
    /// </summary>
    public class PharmacyDepartmentOpenMedicineRepoVO
    {
        /// <summary>
        /// Id khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Số hiệu phòng dược
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Id tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Id tổ chức cấp cao nhất
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Số hiệu nhóm lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Người dùng sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối cùng
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người dùng sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Tên phòng dược
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// Tên nhóm lớn
        /// </summary>
        public string dlmc { get; set; }

    }
}
