using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    /// <summary>
    /// Sắp xếp phẫu thuậtđối tượng request
    /// </summary>
    public class ArrangementRequestVO
    {
        public string orgId { get; set; }
        public string lsyzid { get; set; }
    }
}
