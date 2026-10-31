using System;
using Newtouch.CIS.Compat;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// kết chuyểnchi tiết
    /// </summary>
    public class VCarryOverDetailEntity
    {
        /// <summary>
        /// vật tưTên
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// kết chuyểnSố lượng(mangĐơn vị)
        /// </summary>
        public string jzsl { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Nhà cung cấpTên
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Giá bán lẻ tương ứngzhyzĐơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// tổng bán lẻ
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// Khoa phòngTên đơn vị
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Tồn khoSố lượng Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
