using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// chi tiết phiếu lĩnh
    /// </summary>
    public class ApplyDetailVEntity
    {
        /// <summary>
        /// chi tiết phiếu lĩnh ID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// thu phí tên nhóm lớn
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// số lượng lĩnh kèm đơn vị
        /// </summary>
        public string slslStr { get; set; }

        /// <summary>
        /// số lượng lĩnh đơn vị nhỏ nhất
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// số lượng đã phát kèm đơn vị
        /// </summary>
        public string yfslStr { get; set; }

        /// <summary>
        /// số lượng đã phát đơn vị nhỏ nhất
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
