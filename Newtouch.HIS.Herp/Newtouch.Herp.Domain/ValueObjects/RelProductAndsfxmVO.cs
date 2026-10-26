using System;

namespace Newtouch.Herp.Domain.ValueObjects
{
    /// <summary>
    /// Đối chiếu khoản thu vật tư
    /// </summary>
    public class RelProductAndsfxmVo
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Vật tư ID(Bắt buộc nhập)
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// Loại vật tư ID
        /// </summary>
        public string productTypeId { get; set; }

        /// <summary>
        /// Tên loại vật tư
        /// </summary>
        public string productTypeName { get; set; }

        /// <summary>
        /// Mã nhóm thu lớn
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// Tên nhóm thu lớn
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// Mã khoản thu
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// Tên khoản thu
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// Trạng thái  0:Hủy bỏ;1.Hiệu lực
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
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
