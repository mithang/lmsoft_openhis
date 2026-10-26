using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// Nội dung chứng từ
    /// </summary>
    public class DjInfDTO
    {
        /// <summary>
        /// Thông tin bảng chính chứng từ
        /// </summary>
        public KfCrkdjEntity crkdj { get; set; }

        /// <summary>
        /// Thông tin chi tiết chứng từ
        /// </summary>
        public List<KfCrkmxEntity> crkdjmx { get; set; }

        /// <summary>
        /// Chi tiết lĩnh
        /// </summary>
        public List<VApplyBillDetailEntity> applyBillDetail { get; set; }
    }
}
