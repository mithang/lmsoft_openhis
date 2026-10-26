using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.BusinessObjects
{
    public class NodeContentPresBO
    {
        /// <summary>
        /// Bảng chính đơn thuốc
        /// </summary>
        public PrescriptionEntity presEntity { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc
        /// </summary>
        public List<PrescriptionDetailQueryVO> presDetailList { get; set; }

        /// <summary>
        /// Thời gian in đơn thuốc
        /// </summary>
        public DateTime? cfzhdysj { get; set; }

        /// <summary>
        /// Thời gian in phiếu điều trị(Chỉ phục hồi chức năng)
        /// </summary>
        public DateTime? zldzhdysj { get; set; }

        /// <summary>
        /// Đồng bộ tớiHIS của result
        /// </summary>
        public string sendtohisResult { get; set; }
    }
}
