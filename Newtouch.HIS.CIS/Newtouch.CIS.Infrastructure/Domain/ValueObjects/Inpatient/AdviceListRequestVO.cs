using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    /// <summary>
    /// Y lệnhTra cứuGrid request
    /// </summary>
   public class AdviceListRequestVO
    {

        /// <summary>
        /// khingàyY lệnh
        /// </summary>
        public bool dryz { get; set; }
        
        public string yzlb { get; set; }
        /// <summary>
        /// chưaPhê duyệt
        /// </summary>
        public bool wsh { get; set; }
        /// <summary>
        /// Tất cả,Hiệu lực
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
