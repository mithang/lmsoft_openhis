using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    public class VisitNumBO
    {
        /// <summary>
        /// 
        /// </summary>
        public List<OutpatientVisitNumVO> OutpatientList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InpatientVisitNumVO> InpatientList { get; set; }

        /// <summary>
        /// Tổng số người đang nằm viện
        /// </summary>
        public int zyrs { get; set; }

    }

    public class MonthVisitNumBO
    {
        /// <summary>
        /// 
        /// </summary>
        public List<OutpatientWeekVisitNumVO> OutpatientList { get; set; }
        public List<OutpatientWeekVisitNumVO> InpatientList { get; set; }

    }

    public class SCNumBO
    {
        public List<OutpatientSCNumVO> OutpatientList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InpatientSCNumVO> InpatientList { get; set; }
    }
}
