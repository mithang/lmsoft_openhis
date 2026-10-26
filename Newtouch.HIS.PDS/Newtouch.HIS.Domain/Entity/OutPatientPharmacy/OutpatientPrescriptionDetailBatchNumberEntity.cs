using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// ngoại trú chi tiết đơn thuốc số lô
    /// </summary>
    [Table("mz_cfmxph")]
    public class OutpatientPrescriptionDetailBatchNumberEntity : IEntity<OutpatientPrescriptionDetailBatchNumberEntity>
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        [Key]
        public string cfmxphId { get; set; }

        /// <summary>
        /// tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lượng đơn vị nhỏ nhất
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// thuốc số nhóm
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// ngoại trú chi tiết đơn thuốc ID, mz_cfmx.Id
        /// </summary>
        public long? cfmxId { get; set; }

        /// <summary>
        /// trạng thái: 1-còn hiệu lực; 0-không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// tạo người
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 归家状态: 1-đã xếp lên kệ; 0-chưa xếp lên kệ
        /// </summary>
        public string gjzt { get; set; }

        /// <summary>
        /// nhà thuốc phát thuốc mã
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

    }
}
