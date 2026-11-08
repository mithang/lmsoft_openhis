using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Kháng sinh Thuốc thông tin
    /// </summary>
    public class KssYpVO
    {
        /// <summary>
        /// Thuốc Id
        /// </summary>
        public int ypId { get; set; }
        /// <summary>
        /// Thuốc Code
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }
    }
}
