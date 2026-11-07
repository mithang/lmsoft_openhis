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
        /// vật tư Tên
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// kết chuyển Số lượng(mang Đơn vị)
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
        /// Nhà cung cấp Tên
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Giá bán lẻ tương ứngzhyz Đơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// tổng bán lẻ
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// Khoa phòng Tên đơn vị
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Tồn kho Số lượng Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
