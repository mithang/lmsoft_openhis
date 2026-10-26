using System;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// Chi tiết kết chuyển
    /// </summary>
    public class VCarryOverDetailEntity
    {
        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Số lượng kết chuyển(Kèm đơn vị)
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
        /// Tên nhà cung cấp
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Giá bán lẻ Tương ứngzhyzĐơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// Tổng số tiền bán lẻ
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// Tên đơn vị khoa phòng
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Số lượng tồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
