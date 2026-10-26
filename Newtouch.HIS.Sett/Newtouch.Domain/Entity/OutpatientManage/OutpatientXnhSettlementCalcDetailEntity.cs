using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết tính toán quyết toán ngoại trú Bảo hiểm nông thôn mới
    /// </summary>
    [Table("mz_xnh_settcalcDetail")]
    public class OutpatientXnhSettlementCalcDetailEntity : IEntity<OutpatientXnhSettlementCalcDetailEntity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// mz_xnh_settResultKhóa chính bảng
        /// </summary>
        public string resultId { get; set; }

        /// <summary>
        /// Tên tính toán
        /// </summary>
        public string calcName { get; set; }

        /// <summary>
        /// Giải thích tính toán
        /// </summary>
        public string calcMemo { get; set; }

        /// <summary>
        /// Tính toán lần này
        /// </summary>
        public string calcBefore { get; set; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public string calcAfter { get; set; }

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