using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Kháng sinh Loại
    /// </summary>
    public class KssYpLbVO
    {
        /// <summary>
        /// Kháng sinh Loạiid
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Kháng sinh Tên loại
        /// </summary>
        public string typeName { get; set; }
    }
}
