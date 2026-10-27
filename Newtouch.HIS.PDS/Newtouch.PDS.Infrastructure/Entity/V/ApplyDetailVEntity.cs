using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// Chi tiết đơn lĩnh
    /// </summary>
    public class ApplyDetailVEntity
    {
        /// <summary>
        /// Chi tiết đơn lĩnhID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Tên loại thu phí lớn
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Số lượng lĩnh kèm đơn vị
        /// </summary>
        public string slslStr { get; set; }

        /// <summary>
        /// Số lượng lĩnh Đơn vị nhỏ nhất
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// Số lượng đã phát kèm đơn vị
        /// </summary>
        public string yfslStr { get; set; }

        /// <summary>
        /// Số lượng đã phát Đơn vị nhỏ nhất
        /// </summary>
        public int yfsl { get; set; }

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
