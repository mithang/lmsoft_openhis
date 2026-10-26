using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutOrInStoredOperate
{
    /// <summary>
    /// gửi đơn chứng duyệt tham số
    /// </summary>
    public class OutOrInStorageBillApprovalDTO
    {
        /// <summary>
        /// ra nhập kho ID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// loại chứng từ
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// duyệt viên
        /// </summary>
        public string auditor { get; set; }

        /// <summary>
        /// chứng từ nhập/xuất kho chi tiết
        /// </summary>
        public List<SysMedicineStorageIOReceiptDetailEntity> djmx { get; set; }

        /// <summary>
        /// chứng từ nhập/xuất kho chính bảng thông tin
        /// </summary>
        public SysMedicineStorageIOReceiptEntity dj { get; set; }
    }
}
