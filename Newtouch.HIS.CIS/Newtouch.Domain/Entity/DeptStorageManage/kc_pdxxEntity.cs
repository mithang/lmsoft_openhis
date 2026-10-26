using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity.DeptStorageManage
{
    /// <summary>
    /// Bảng kiểm kê tồn kho
    /// </summary>
    [Table("dept_pdxx")]
    public class KcPdxxEntity : IEntity<KcPdxxEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// KhoID
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        ///  Thời gian bắt đầu
        /// </summary>
        public DateTime kssj { get; set; }

        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime? jssj { get; set; }

        /// <summary>
        /// Cách kiểm kê
        /// </summary>
        public short pdfs { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
