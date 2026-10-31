using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Chứng chỉ_Loại chứng chỉ
    /// </summary>
    [SugarTable("lic_licenceType")]
    public class LicLicenceTypeEntity : IEntity<LicLicenceTypeEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Tên loại Giấy phép kinh doanh,Quyền đại lý,Giấy đăng ký...
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Thuộc về Id Vật tư,Nhà cung cấp
        /// </summary>
        public string belongedId { get; set; }

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
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
