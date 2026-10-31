using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Vật tư_Chênh lệch điều chỉnh giá
    /// </summary>
    [SugarTable("wz_priceAdjustmentProfitAndLoss")]
    public class WzPriceAdjustmentProfitAndLossEntity : IEntity<WzPriceAdjustmentProfitAndLossEntity>
    {
        /// <summary>
        /// Khóa chính Số thứ tự chênh lệch điều chỉnh giá
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string TjsyId { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Kho ID
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô hàng
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Loại sửa đổi
        /// </summary>
        public DateTime tjsj { get; set; }

        /// <summary>
        /// Tệp điều chỉnh giá
        /// </summary>
        public string tjwj { get; set; }

        /// <summary>
        /// Số lượng lúc đó
        /// </summary>
        public int dssl { get; set; }

        /// <summary>
        /// Giá bán lẻ gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal ylsj { get; set; }

        /// <summary>
        /// Giá bán lẻ mới
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal xlsj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Lợi nhuận điều chỉnh giá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsjtjlr { get; set; }
        
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
