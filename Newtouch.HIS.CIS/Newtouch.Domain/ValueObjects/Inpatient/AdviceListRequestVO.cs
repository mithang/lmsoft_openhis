using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    /// <summary>
    /// Tra cứu y lệnhGrid request
    /// </summary>
   public class AdviceListRequestVO
    {

        /// <summary>
        /// Y lệnh trong ngày
        /// </summary>
        public bool dryz { get; set; }
        
        public string yzlb { get; set; }
        /// <summary>
        /// Chưa duyệt
        /// </summary>
        public bool wsh { get; set; }
        /// <summary>
        /// Tất cả,Có hiệu lực
        /// </summary>
        public string yx { get; set; }
        public DateTime kssj { get; set; }
        public DateTime jssj { get; set; }
        public int yzlx { get; set; }
        public string orgId { get; set; }
        public string zyh { get; set; }
		public string deptName { get; set; }
    }
}
