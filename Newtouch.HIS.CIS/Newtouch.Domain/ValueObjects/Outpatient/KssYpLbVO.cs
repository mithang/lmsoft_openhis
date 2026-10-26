using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Loại kháng sinh
    /// </summary>
    public class KssYpLbVO
    {
        /// <summary>
        /// Loại kháng sinhid
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Tên loại kháng sinh
        /// </summary>
        public string typeName { get; set; }
    }
}
