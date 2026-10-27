using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutOrInStoredOperate
{
    /// <summary>
    /// Tham Số gửi duyệt chứng từ
    /// </summary>
    public class OutOrInStorageBillApprovalDTO
    {
        /// <summary>
        /// Xuất nhập khoID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Người duyệt
        /// </summary>
        public string auditor { get; set; }

        /// <summary>
        /// Chi tiết chứng từ xuất nhập kho
        /// </summary>
        public List<SysMedicineStorageIOReceiptDetailEntity> djmx { get; set; }

        /// <summary>
        /// Thông tin bảng chính chứng từ xuất nhập kho
        /// </summary>
        public SysMedicineStorageIOReceiptEntity dj { get; set; }
    }
}
