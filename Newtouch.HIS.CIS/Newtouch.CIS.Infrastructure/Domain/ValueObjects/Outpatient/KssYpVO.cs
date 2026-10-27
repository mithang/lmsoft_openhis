using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Kháng sinhThuốcthông tin
    /// </summary>
    public class KssYpVO
    {
        /// <summary>
        /// ThuốcId
        /// </summary>
        public int ypId { get; set; }
        /// <summary>
        /// ThuốcCode
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }
    }
}
