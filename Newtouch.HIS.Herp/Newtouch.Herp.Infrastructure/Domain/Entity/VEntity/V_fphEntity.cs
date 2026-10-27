using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thông tin chính chứng từ nhập/xuất kho
    /// </summary>
    public class V_fphEntity
    {
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }
        /// <summary>
        /// Thời gian xuất hóa đơn
        /// </summary>
        public string kprq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gysCode { get; set; }
        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }

    }
}
