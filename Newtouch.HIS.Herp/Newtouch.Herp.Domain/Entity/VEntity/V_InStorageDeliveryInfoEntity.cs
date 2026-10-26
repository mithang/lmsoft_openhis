using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thông tin phiếu giao hàng nhập kho
    /// </summary>
    public class VInStorageDeliveryInfoEntity
    {

        /// <summary>
        /// Số phiếu giao hàng
        /// </summary>
        public string deliveryNo { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Nhà cung cấp ID
        /// </summary>
        public string gysId { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// Hình thức nhập/xuất kho ID
        /// </summary>
        public string crkfsId { get; set; }

        /// <summary>
        /// Tên hình thức nhập/xuất kho
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }
    }
}
