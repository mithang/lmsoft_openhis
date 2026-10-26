using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.DTO
{
    /// <summary>
    /// Gửi mua hàng
    /// </summary>
    public class SubmitGeneratingPurchaseDto
    {
        /// <summary>
        /// Chi tiết đơn mua hàng
        /// </summary>
        public List<VCgPurchaseOrderDetailEntity> cgmx { get; set; }

        /// <summary>
        /// Nhân viên thao tác
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Tổ chức ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }
    }
}