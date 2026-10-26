using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Quyết toán BHYT nội trú Thông tin bệnh nhân
    /// </summary>
    public class InpatientSettYbPatInfoVO
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Số khám bệnh
        /// </summary>
        public string jzbh { get; set; }
        /// <summary>
        /// Số BHXH
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// Mã trung tâm chi nhánh
        /// </summary>
        public string fzxbh { get; set; }
        /// <summary>
        /// Loại thanh toán
        /// </summary>
        public string zhifuleibie { get; set; }
        /// <summary>
        /// Phương pháp BHXH
        /// </summary>
        public string sbbf { get; set; }
    }
}
