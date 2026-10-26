using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Phụ thêm tính phí bệnh nhân hệ thống(17Tầng)
    /// </summary>
    [Table("xt_brsffj")]
    public class SysPatientChargeAdditionalEntity : IEntity<SysPatientChargeAdditionalEntity>
    {
        /// <summary>
        /// Số phụ thêm tính phí bệnh nhân
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int brsffjbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// Loại lớn hiển thị bổ sung
        /// </summary>
        public string fjxsdl { get; set; }

        /// <summary>
        /// Tỷ lệ phí dịch vụ
        /// </summary>
        public decimal fwfbl { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// Loại lớn
        /// </summary>
        public string dl { get; set; }
        /// <summary>
        /// Hạng mục thu phí
        /// </summary>
        public string sfxm { get; set; }

    }
}
