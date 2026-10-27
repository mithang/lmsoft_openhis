using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Chứng từ nhập/xuất kho
    /// </summary>
    [SugarTable("kf_applyOrderDetail")]
    public class KfApplyOrderDetailEntity : IEntity<KfApplyOrderDetailEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Phiếu lĩnh ID
        /// </summary>
        public long sldId { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lượng (Số lượng*zhyz=Số lượng đơn vị nhỏ nhất)
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Số lượng đã phát (Số lượng đã phát*zhyz=Số lượng đơn vị nhỏ nhất)
        /// </summary>
        public int? yfsl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }
        
        /// <summary>
        /// Trạng thái 0:Hủy bỏ；1.Hiệu lực
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
