using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Bảng liên kết số thứ tự bồi thường ngoại trú Bảo hiểm nông thôn mới
    /// </summary>
    [Table("mz_xnh_outpIdRel")]
    public class OutpatientXnhOutpIdRelEntity : IEntity<OutpatientXnhOutpIdRelEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã nội bộ đăng ký khám
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// Số thứ tự bồi thường
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Trạng thái xử lý 0-Quay lại ngoại trú 1-Chi tiết đã tải lên 2-Đã quyết toán 3-Đã đảo hóa đơn đỏ 
        /// </summary>
        public int processingStatus { get; set; }

        /// <summary>
        /// Trạng thái 1-Hiệu lực  0-Không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}