using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.BusinessObjects
{
    public class NodeContentPresBO
    {
        /// <summary>
        /// Đơn thuốcchínhbảng
        /// </summary>
        public PrescriptionEntity presEntity { get; set; }

        /// <summary>
        /// Đơn thuốcchi tiết
        /// </summary>
        public List<PrescriptionDetailQueryVO> presDetailList { get; set; }

        /// <summary>
        /// Đơn thuốcInThời gian
        /// </summary>
        public DateTime? cfzhdysj { get; set; }

        /// <summary>
        /// Điều trịphiếuInThời gian(chỉPhục hồi chức năng)
        /// </summary>
        public DateTime? zldzhdysj { get; set; }

        /// <summary>
        /// Đồng bộđếnHIS của result
        /// </summary>
        public string sendtohisResult { get; set; }
    }
}
