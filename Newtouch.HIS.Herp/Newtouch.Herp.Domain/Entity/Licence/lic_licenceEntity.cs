using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Giấy phép _ Loại giấy phép
    /// </summary>
    [Table("lic_licence")]
    public class LicLicenceEntity : IEntity<LicLicenceEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Thuộc về Id Vật tư,Nhà cung cấp
        /// </summary>
        public string belongedId { get; set; }

        /// <summary>
        /// Nhà cung cấp ID,Vật tư ID
        /// </summary>
        public string objectId { get; set; }

        /// <summary>
        /// Tên đối tượng giấy phép
        /// </summary>
        public string objectName { get; set; }

        /// <summary>
        /// Tên giấy phép,Loại giấy tờ Id
        /// </summary>
        public string licenceTypeId { get; set; }

        /// <summary>
        /// Số giấy phép
        /// </summary>
        public string licenceNo { get; set; }

        /// <summary>
        /// Ngày có hiệu lực
        /// </summary>
        public DateTime? qxrq { get; set; }

        /// <summary>
        /// Ngày hết hiệu lực
        /// </summary>
        public DateTime? sxrq { get; set; }

        /// <summary>
        /// Địa chỉ tệp
        /// </summary>
        public string fileUrl { get; set; }

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
