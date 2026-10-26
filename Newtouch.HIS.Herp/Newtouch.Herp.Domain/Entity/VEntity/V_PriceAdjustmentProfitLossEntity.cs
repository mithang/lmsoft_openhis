using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Gửi thông tin lãi/lỗ
    /// </summary>
    public class VPriceAdjustmentProfitLossEntity
    {
        /// <summary>
        /// Lãi/lỗ điều chỉnh giá ID
        /// </summary>
        public string TjsyId { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Kho
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Thời gian điều chỉnh giá
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
        /// Tên đơn vị
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// Giá bán lẻ gốc
        /// </summary>
        public decimal ylsj { get; set; }

        /// <summary>
        /// Giá bán lẻ trước
        /// </summary>
        public decimal xlsj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi Và ylsj,xlsj,dssl Tương ứng
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Lợi nhuận điều chỉnh giá bán lẻ
        /// </summary>
        public decimal lsjtjlr { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }
    }
}
