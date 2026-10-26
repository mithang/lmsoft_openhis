using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Phân khám ngoại trú,Số lượng chờ khám của các phòng khám
    /// </summary>
    public class OutpatientConsultCountVO
    {
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Tên phòng khám
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// Số lượng chờ khám còn lại
        /// </summary>
        public int num { get; set; }
    }
}
