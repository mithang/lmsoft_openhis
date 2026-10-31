using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Số lô chi tiết đơn ngoại trú
    /// </summary>
    [Table("mz_cfmxph")]
    [SugarTable("mz_cfmxph")]
    public class OutpatientPrescriptionDetailBatchNumberEntity : IEntity<OutpatientPrescriptionDetailBatchNumberEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string cfmxphId { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lượng đơn vị nhỏ nhất
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Số nhóm thuốc
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// Chi tiết đơn ngoại trúID,mz_cfmx.Id
        /// </summary>
        public long? cfmxId { get; set; }

        /// <summary>
        /// Trạng thái:1-Hiệu lực ;0-Không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Trạng thái về nhà:1-Đã xếp lên kệ;0-Chưa xếp lên kệ
        /// </summary>
        public string gjzt { get; set; }

        /// <summary>
        /// Mã nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

    }
}
