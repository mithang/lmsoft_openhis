using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.BusinessObjects
{
    public class NodeContentPresBO
    {
        /// <summary>
        /// Đơn thuốc chính bảng
        /// </summary>
        public PrescriptionEntity presEntity { get; set; }

        /// <summary>
        /// Đơn thuốc chi tiết
        /// </summary>
        public List<PrescriptionDetailQueryVO> presDetailList { get; set; }

        /// <summary>
        /// Đơn thuốc In Thời gian
        /// </summary>
        public DateTime? cfzhdysj { get; set; }

        /// <summary>
        /// Điều trị phiếu In Thời gian(chỉ Phục hồi chức năng)
        /// </summary>
        public DateTime? zldzhdysj { get; set; }

        /// <summary>
        /// Đồng bộ đến HIS của result
        /// </summary>
        public string sendtohisResult { get; set; }
    }
}
